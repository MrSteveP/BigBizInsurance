using CrossCutting.Core;
using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Persistance;
using UserManagement.Application.Persistance.Abstraction;
using UserManagement.Persistance.Concrete;

namespace UserManagement.Persistance
{
    public class UserManagementUnitOfWork :IUserManagementUnitOfWork
    {

        public UserManagementUnitOfWork(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.Context = context;
            HttpContextAccessor = httpContextAccessor;
            this.Logger = ApplicationLogging.CreateLogger<UserManagementUnitOfWork>() as ILogger<UserManagementUnitOfWork>;

        }

        ~UserManagementUnitOfWork()
        {
            Dispose(true);
        }

        private ApplicationDbContext Context { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public ILogger<UserManagementUnitOfWork> Logger { get; private set; }
        public string CurrentUserName { get; set; }

        #region Repositories

        IPolicyRepository policyRepository;
        public IPolicyRepository PolicyRepository
        {
            get
            {
                if (policyRepository == null)
                    policyRepository = new PolicyRepository(Context);

                return policyRepository;
            }
        }


        IApplicationPolicyRoleRepository applicationPolicyRoleRepository;
        public IApplicationPolicyRoleRepository ApplicationPolicyRoleRepository
        {
            get
            {
                if (applicationPolicyRoleRepository == null)
                    applicationPolicyRoleRepository = new ApplicationPolicyRoleRepository(Context);

                return applicationPolicyRoleRepository;
            }
        }

        #endregion



        public int Save(Guid? userId = null, bool validateOnSaveEnabled = true)
        {
            var changeTracker = this.Context.ChangeTracker;
            var entities = from entry in changeTracker.Entries()
                           where entry.State == EntityState.Modified || entry.State == EntityState.Added
                           select entry.Entity;

            var validationResults = new List<ValidationResult>();

            foreach (var entity in entities)
            {
                string validationErrors = null;

                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
                {
                    var exception = new ValidationException();

                    foreach (var item in validationResults)
                    {
                        validationErrors +=
                            $"Entity: {entity} - Property: {item.MemberNames} - Error: {item.ErrorMessage} \n ";
                        Logger?.LogError(validationErrors);
                    }

                    throw new ValidationException(validationErrors);
                }
            }

            this.UpdatePropertiesBeforeSave(userId?.ToString());
            int x = 0;
            try
            {
                x = this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return x;
        }

        /// <summary>
        /// Updates the properties before save.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <summary>
        /// Updates the properties before save.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// 
        private void UpdatePropertiesBeforeSave(string userId = null)
        {
            userId = userId ?? this.CurrentUserName;
            if (string.IsNullOrEmpty(userId) && this.HttpContextAccessor?.HttpContext?.User != null
                                             && (this.HttpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false))
            {
                userId = this.HttpContextAccessor?.HttpContext?.User?.Identity?.Name;
            }

            userId = userId ?? "Anonymous";

            const string CreatedOnProperty = "CreatedOn";
            const string ModifiedOnProperty = "UpdatedOn";

           // const string IdProperty = "Id";

            var entitiesWithCreatedOn = this.Context.ChangeTracker.Entries().Where(
                e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedOnProperty) != null);
            foreach (var entry in entitiesWithCreatedOn)
            {
                entry.Property(CreatedOnProperty).CurrentValue = DateTime.Now;
            }


            //var entitiesWithId = this.Context.ChangeTracker.Entries().Where(
            //    e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(IdProperty) != null);
            //foreach (var entry in entitiesWithId)
            //{
            //    Guid id;
            //    if (Guid.TryParse(entry.Property(IdProperty).CurrentValue.ToString(), out id) && id == Guid.Empty)
            //    {
            //        entry.Property(IdProperty).CurrentValue = Guid.NewGuid().AsSequentialGuid();
            //    }
            //}

            var entitiesWithModifiedOn = this.Context.ChangeTracker.Entries().Where(
                e => e.State == EntityState.Modified && e.Entity.GetType().GetProperty(ModifiedOnProperty) != null);
            foreach (var entry in entitiesWithModifiedOn)
            {
                // Yousra: Mark CreatedOnProperty as not modified in update to preserve the original value inserted in DB 
                // especially in case you donot submit the whole model from view and send specific data
                entry.Property(CreatedOnProperty).IsModified = false;

                // ----------------------------
                entry.Property(ModifiedOnProperty).CurrentValue = DateTime.Now;
            }

            if (/*!string.IsNullOrEmpty(userId)*/userId != null)
            {
                const string CreatedByProperty = "CreatedBy";
                const string ModifiedByProperty = "UpdatedBy";
                var entitiesWithCreatedBy = this.Context.ChangeTracker.Entries().Where(
                    e => e.State == EntityState.Added && e.Entity.GetType().GetProperty(CreatedByProperty) != null);
                foreach (var entry in entitiesWithCreatedBy)
                {
                    entry.Property(CreatedByProperty).CurrentValue = userId;
                }

                var entitiesWithModifiedBy = this.Context.ChangeTracker.Entries().Where(
                          e => e.State == EntityState.Modified && e.Entity.GetType().GetProperty(ModifiedByProperty) != null);
                foreach (var entry in entitiesWithModifiedBy)
                {
                    // Yousra: Mark CreatedByProperty as not modified in update to preserve the original value inserted in DB 
                    // especially in case you donot submit the whole model from view and send specific data
                    entry.Property(CreatedByProperty).IsModified = false;

                    // ----------------------------
                    entry.Property(ModifiedByProperty).CurrentValue = userId;
                }
            }
        }

        //public int Save()
        //{
        //    return this.Context.SaveChanges();
        //}

        //public async Task<int> SaveAsync()
        //{
        //    return await this.Context.SaveChangesAsync();
        //}


        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
