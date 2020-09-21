using AuditTrailComponent.Persistance.Enumerations;
using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using AuditTrailComponent.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Concrete
{
    sealed internal class AuditTrailService : IAuditTrailService
    {

        IAuditUnitOfWork AuditUnitOfWork { get; }
        INotificationInternalService notificationService { get; }
        IErrorLogInternalService ErrorLoger { get; }

        public AuditTrailService(INotificationInternalService _notificationService,
            IAuditUnitOfWork _auditUnitOfWork,
            IErrorLogInternalService errorLoger)
        {
            notificationService = _notificationService;
            AuditUnitOfWork = _auditUnitOfWork;
            ErrorLoger = errorLoger;
        }


        public void IntiateActions(List<AuditActionDto> businessActionDtos)
        {
            try
            {
                ActionsRegisteration actionRegisteration = new ActionsRegisteration(AuditUnitOfWork, ErrorLoger);
                actionRegisteration.Register(businessActionDtos);
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }

        //3 database hits
        public async Task SaveCustomActionsAuditTrailAsync(string actionCode, string actionData, string userName)
        {
            try
            {
               
                AuditTrail auditTrail = new AuditTrail
                {
                    Audit_ActionCode = actionCode,
                    ActionData = actionData,
                    UserName = userName
                };

                await AuditUnitOfWork.AuditTrailRepository.AddAsync(auditTrail);
                await notificationService.SaveNotificationAsync(actionCode);
                await AuditUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        //4 database hits
        public async Task SaveDbActionsAuditTrailAsync(List<DatabaseChangesDto> databaseChanges)
        {
            try
            {
                List<AuditTrail> auditTrails = new List<AuditTrail>();
                var updatedList = AuditUnitOfWork.AuditTrailRepository.AddToAuditTrailList(databaseChanges, DatabaseAuditActionsEnum.DatabaseUpdate.ToString(), EntityState.Modified, auditTrails);
                var addedList = AuditUnitOfWork.AuditTrailRepository.AddToAuditTrailList(databaseChanges, DatabaseAuditActionsEnum.DatabaseAdd.ToString(), EntityState.Added, auditTrails);
                var deletedList = AuditUnitOfWork.AuditTrailRepository.AddToAuditTrailList(databaseChanges, DatabaseAuditActionsEnum.DatabaseDelete.ToString(), EntityState.Deleted, auditTrails);

                await AuditUnitOfWork.AuditTrailRepository.AddAsync(auditTrails);
                await notificationService.SaveNotificationsAsync(DatabaseAuditActionsEnum.DatabaseUpdate.ToString(), updatedList);
                await notificationService.SaveNotificationsAsync(DatabaseAuditActionsEnum.DatabaseAdd.ToString(), addedList);
                await notificationService.SaveNotificationsAsync(DatabaseAuditActionsEnum.DatabaseDelete.ToString(), deletedList);
                await AuditUnitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }


        public async Task<AuditTrailFilterDto> Search(AuditTrailFilterDto model)
        {
            var expression = GetSearchExpression(model);
            (var query, int totalCount) = await AuditUnitOfWork.AuditTrailRepository.GetPagedByFiltersAsync(
                model.PageNumber,
                model.jtPageSize.Value,
                expression,
                a => a.OrderByDescending(d => d.Date));

            model.Items = query.Select(auditTrail => AuditTrailMapper.MapToDto(auditTrail)).ToList();

            model.TotalCount = totalCount;

            return model;
        }

        public async Task<AuditTrailDto> GetById(int id)
        {
            var audit=await AuditUnitOfWork.AuditTrailRepository.GetByIdAsync(id);

            if (audit == null) return null;

            return audit;
        }


        List<Expression<Func<AuditTrail, bool>>> GetSearchExpression(AuditTrailFilterDto model)
        {
            List<Expression<Func<AuditTrail, bool>>> filterList = new List<Expression<Func<AuditTrail, bool>>>();


            if (!string.IsNullOrEmpty(model.UserName))
                filterList.Add(c => c.UserName.Contains(model.UserName));

            if (!string.IsNullOrEmpty(model.Audit_ActionCode))
                filterList.Add(c => c.Audit_ActionCode.Contains(model.Audit_ActionCode));

            if (!string.IsNullOrEmpty(model.EntityName))
                filterList.Add(c => c.EntityName.Contains(model.EntityName));

            if (model.FromDate != null)
                filterList.Add(c => c.Date.Date >= model.FromDate);

            if (model.ToDate != null)
                filterList.Add(c => c.Date.Date <= model.ToDate);

            return filterList;

        }


    }
}
