using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
namespace AuditTrailComponent.Services.Concrete
{
    sealed internal class BusinessActionsRegistration
    {
        public List<AuditActionDto> BusinessActionDtos { get; }
        public List<Persistance.Models.Action> Actions { get; }
        public List<ActionUserGroup> ActionUserGroups { get; }
        public IAuditUnitOfWork AuditUnitOfWork { get; }
        public IErrorLogInternalService ErrorLoger { get; }

        public BusinessActionsRegistration(List<AuditActionDto> businessActionDtos,
            List<Persistance.Models.Action> actions,
            List<ActionUserGroup> actionUserGroups,
            IAuditUnitOfWork auditUnitOfWork,
            IErrorLogInternalService errorLoger)
        {
            BusinessActionDtos = businessActionDtos;
            Actions = actions;
            ActionUserGroups = actionUserGroups;
            AuditUnitOfWork = auditUnitOfWork;
            ErrorLoger = errorLoger;
        }
        public void AddBusinessActions()
        {
            try
            {
                if (BusinessActionDtos.Any())
                {
                    BusinessActionDtos.ForEach(actionCode =>
                    {
                        AddOrUpdateActions(actionCode, Actions);
                        AddOrUpdateActionUserGroups(actionCode, ActionUserGroups);
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }

        void AddOrUpdateActionUserGroups(AuditActionDto actionCode, List<ActionUserGroup> actionUserGroups)
        {
            try
            {
                if (actionCode.SendNotification)
                {
                    var actionUserGroup = AuditUnitOfWork.ActionUserGroupRepository.GetFirst(a => a.Audit_ActionCode == actionCode.ActionCode);
                    if (actionUserGroup != null)
                    {
                        FillActionUserGroup(actionCode, actionUserGroup);

                        AuditUnitOfWork.ActionUserGroupRepository.Update(actionUserGroup);
                    }
                    else
                    {
                        actionUserGroup = new ActionUserGroup();
                        actionUserGroup.Audit_ActionCode = actionCode.ActionCode;
                        FillActionUserGroup(actionCode, actionUserGroup);

                        actionUserGroups.Add(actionUserGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }

        void FillActionUserGroup(AuditActionDto actionCode, ActionUserGroup actionUserGroup)
        {
            actionUserGroup.From = actionCode.NotificationFromEmail;
            actionUserGroup.ToEmails = actionCode.NotificationEmails;
            actionUserGroup.Subject = actionCode.NotificationSubject;
            actionUserGroup.Message = actionCode.NotificationMessage;
        }

        void AddOrUpdateActions(AuditActionDto actionCode, List<Persistance.Models.Action> actions)
        {
            try
            {
                var action = AuditUnitOfWork.ActionRepository.GetFirst(a => a.Code == actionCode.ActionCode);
                if (action != null)
                {
                    action.SendNotification = actionCode.SendNotification;
                    AuditUnitOfWork.ActionRepository.Update(action);
                }
                else
                {
                    action = new Persistance.Models.Action() { Code = actionCode.ActionCode, SendNotification = actionCode.SendNotification };
                    actions.Add(action);
                }
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }
    }

}
