using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using AuditTrailComponent.Services.Mapper;
using AuditTrailComponent.Services.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Concrete
{
    sealed internal class NotificationService : INotificationInternalService, INotificationPublicService
    {

        IAuditUnitOfWork AuditUnitOfWork { get; }
        IErrorLogInternalService ErrorLoger { get; }
        IEmailService EmailService { get; }

        public NotificationService(IAuditUnitOfWork auditUnitOfWork,
            IErrorLogInternalService errorLoger,
            IEmailService emailService)
        {
            AuditUnitOfWork = auditUnitOfWork;
            ErrorLoger = errorLoger;
            EmailService = emailService;
        }


        public async Task CheckNotificationsAndSend()
        {
            try
            {


                var notSentNotifications = await AuditUnitOfWork.NotificationRepository.GetAsync(a => a.Sent == false);
                if (notSentNotifications.Any())
                {
                    List<NotificationLog> notificationLogs = new List<NotificationLog>();

                    notSentNotifications.ForEach(async notification =>
                    {
                        var result = await EmailService.SendAsync("", notification.ToEmails.Split(',').ToList(), "", notification.Message);

                        if (result.Succeeded)
                        {
                            notification.Sent = result.Succeeded;
                            AuditUnitOfWork.NotificationRepository.Update(notification);
                        }

                        NotificationLog notificationLog = new NotificationLog();
                        notificationLog.NotificationId = notification.Id;
                        notificationLog.SendSucceeded = result.Succeeded;
                        notificationLog.ErrorMessage = result.ErrorMessage;
                        notificationLog.TryDate = DateTime.Now;
                        notificationLogs.Add(notificationLog);
                    });

                    await AuditUnitOfWork.NotificationLogRepository.AddAsync(notificationLogs);
                    await AuditUnitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        public async Task SaveNotificationsAsync(string actionCode, List<AuditTrail> auditList)
        {
            try
            {
                var action = AuditUnitOfWork.ActionRepository.GetBySelector(a => new ActionSendNotificationProj { SendNotification = a.SendNotification }, a => a.Code == actionCode).FirstOrDefault();
                if (action != null && action.SendNotification)
                {
                    if (auditList.Count > 0)
                        foreach (var audit in auditList)
                            await SaveNotificationAsync(actionCode, audit.EntityName);
                }
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        public async Task SaveNotificationAsync(string actionCode)
        {
            try
            {
                var action = await AuditUnitOfWork.ActionRepository.GetFirstAsync(a => a.Code == actionCode);
                if (action.SendNotification)
                {
                    var ActionUserGroup = await AuditUnitOfWork.ActionUserGroupRepository.GetByActionCode(actionCode);
                    await CreateNotification(actionCode, ActionUserGroup);
                }
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        public async Task SaveNotificationAsync(string actionCode, string EntityName)
        {
            try
            {
                var action = await AuditUnitOfWork.ActionRepository.GetFirstAsync(a => a.Code == actionCode);
                if (action.SendNotification)
                {
                    var ActionUserGroup = await AuditUnitOfWork.ActionUserGroupRepository.GetByActionCode_EntityName(actionCode, EntityName);
                    await CreateNotification(actionCode, ActionUserGroup, EntityName);
                }
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        async Task CreateNotification(string actionCode, ActionUserGroup ActionUserGroup, string EntityName = "")
        {
            try
            {
                if (ActionUserGroup != null)
                {
                    Notification Notification = new Notification
                    {
                        Audit_ActionCode = actionCode,
                        From = ActionUserGroup.From,
                        Subject = ActionUserGroup.Subject,
                        ToEmails = ActionUserGroup.ToEmails,
                        Message = ActionUserGroup.Message + (!string.IsNullOrEmpty(EntityName) ? $" - EntityName : {EntityName}" : ""),
                        Sent = false
                    };

                    await AuditUnitOfWork.NotificationRepository.AddAsync(Notification);
                }
            }
            catch (Exception ex)
            {
                await ErrorLoger.Log(ex);
            }
        }

        public async Task<NotificationFilterDto> SearchNotifications(NotificationFilterDto model)
        {
            var expression = GetSearchExpressionNotifications(model);
            (var query, int totalCount) = await AuditUnitOfWork.NotificationRepository.GetPagedByFiltersAsync(
                model.PageNumber,
                model.jtPageSize.Value,
                expression,
                a => a.OrderByDescending(d => d.Date));

            model.Items = query.Select(notification => NotificationMapper.MapToDto(notification)).ToList();

            model.TotalCount = totalCount;

            return model;
        }

        List<Expression<Func<Notification, bool>>> GetSearchExpressionNotifications(NotificationFilterDto model)
        {
            List<Expression<Func<Notification, bool>>> filterList = new List<Expression<Func<Notification, bool>>>();


            if (!string.IsNullOrEmpty(model.From))
                filterList.Add(c => c.From.Contains(model.From));

            if (model.Sent != null)
                filterList.Add(c => c.Sent == model.Sent);

            if (!string.IsNullOrEmpty(model.Subject))
                filterList.Add(c => c.Subject.Contains(model.Subject));

            if (!string.IsNullOrEmpty(model.ToEmails))
                filterList.Add(c => c.ToEmails.Contains(model.ToEmails));

            if (!string.IsNullOrEmpty(model.Audit_ActionCode))
                filterList.Add(c => c.Audit_ActionCode.Contains(model.Audit_ActionCode));

            if (model.FromDate != null)
                filterList.Add(c => c.Date.Date >= model.FromDate);

            if (model.ToDate != null)
                filterList.Add(c => c.Date.Date <= model.ToDate);

            return filterList;

        }




        public async Task<NotificationLogFilterDto> SearchNotificationLogs(NotificationLogFilterDto model)
        {
            var expression = GetSearchExpressionNotificationLog(model);
            (var query, int totalCount) = await AuditUnitOfWork.NotificationLogRepository.GetPagedByFiltersAsync(
                model.PageNumber,
                model.jtPageSize.Value,
                expression,
                a => a.OrderByDescending(d => d.TryDate));

            model.Items = query.Select(notification => NotificationLogMapper.MapToDto(notification)).ToList();

            model.TotalCount = totalCount;

            return model;
        }

        List<Expression<Func<NotificationLog, bool>>> GetSearchExpressionNotificationLog(NotificationLogFilterDto model)
        {
            List<Expression<Func<NotificationLog, bool>>> filterList = new List<Expression<Func<NotificationLog, bool>>>();

            if (model.SendSucceeded != null)
                filterList.Add(c => c.SendSucceeded == model.SendSucceeded);

            if (model.FromTryDate != null)
                filterList.Add(c => c.TryDate.Date >= model.FromTryDate);

            if (model.ToTryDate != null)
                filterList.Add(c => c.TryDate.Date <= model.ToTryDate);

            return filterList;

        }
    }
}
