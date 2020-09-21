using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using CrossCutting.Persistance.UnitOfWork;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BigBizInsurance.Application.Extensions;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Application.Persistance.Abstraction;
using BigBizInsurance.Persistance.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBizInsurance.Persistance
{
    public sealed class BigBizInsuranceUnitOfWork : UnitOfWork, IBigBizInsuranceUnitOfWork
    {
        public BigBizInsuranceUnitOfWork(BigBizInsuranceDbContext context,
            IHttpContextAccessor httpContextAccessor, IAuditTrailService auditTrailService)
             : base(context, httpContextAccessor)
        {
            this.Context = context;
            AuditTrailService = auditTrailService;

            CurrentUserName = this.HttpContextAccessor?.HttpContext?.User?.Identity?.Name; ;
        }

        ~BigBizInsuranceUnitOfWork()
        {
            Dispose(true);
        }

        private BigBizInsuranceDbContext Context { get; }
        public IAuditTrailService AuditTrailService { get; }

        public new string CurrentUserName { get; }


        ISampleRepository sampleRepository;
        public ISampleRepository SampleRepository
        {
            get
            {
                if (sampleRepository == null)
                    sampleRepository = new SampleRepository(Context);

                return sampleRepository;
            }
        }

        public new int Save(Guid? userId = null, bool validateOnSaveEnabled = true)
        {
            //TODO : add audit trails code
            UpdateTenantPropertyBeforeSave();

            string userName = this.HttpContextAccessor?.HttpContext?.User?.Identity?.Name;

            //save dbchanges
            var dbChanges = Context.ChangeTracker.Entries()
            .Select(a =>
            new DatabaseChangesDto
            {
                Entity = a.Entity,
                OriginalValues = a.OriginalValues.ToObject(),
                CurrentValues = a.CurrentValues.ToObject(),
                State = a.State,
                UserName = userName
            }).ToList();
            BackgroundJob.Enqueue(() => AuditTrailService.SaveDbActionsAuditTrailAsync(dbChanges));

            return base.Save(userId, validateOnSaveEnabled);
        }

        private void UpdateTenantPropertyBeforeSave()
        {
            const string TenantIdProperty = "TenantId";

            var entitiesInsertWithTenantId = this.Context.ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(TenantIdProperty) != null);
            SetValue(TenantIdProperty, entitiesInsertWithTenantId);

            var entitiesEditWithTenantId = this.Context.ChangeTracker.Entries().Where(
               e => e.State == EntityState.Modified && e.Entity.GetType().GetProperty(TenantIdProperty) != null);
            SetValue(TenantIdProperty, entitiesEditWithTenantId);

        }

        private void SetValue(string TenantIdProperty, IEnumerable<EntityEntry> entitiesInsertWithTenantId)
        {
            foreach (var entry in entitiesInsertWithTenantId)
            {
                int? tenantId = this.HttpContextAccessor?.HttpContext?.User.GetTenantId();
                if (tenantId.HasValue && tenantId.Value > 0)
                    entry.Property(TenantIdProperty).CurrentValue = tenantId;
            }
        }
    }
}
