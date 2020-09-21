using AuditTrailComponent.Persistance.Enumerations;
using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditTrailComponent.Services.Concrete
{
    sealed internal class ActionsRegisteration
    {
        IAuditUnitOfWork AuditUnitOfWork { get; }
        IErrorLogInternalService ErrorLoger { get; }

        public ActionsRegisteration(IAuditUnitOfWork auditUnitOfWork, IErrorLogInternalService errorLoger)
        {
            AuditUnitOfWork = auditUnitOfWork;
            ErrorLoger = errorLoger;
        }
        public void Register(List<AuditActionDto> businessActionDtos)
        {
            try
            {
                List<Persistance.Models.Action> actions = new List<Persistance.Models.Action>();
                List<ActionUserGroup> actionUserGroups = new List<ActionUserGroup>();

                AddDatabaseActions(actions);

                var businessActionsRegistration = new BusinessActionsRegistration(businessActionDtos, actions, actionUserGroups, AuditUnitOfWork, ErrorLoger);
                businessActionsRegistration.AddBusinessActions();

                if (actions.Any())
                    AuditUnitOfWork.ActionRepository.Add(actions);

                if (actionUserGroups.Any())
                    AuditUnitOfWork.ActionUserGroupRepository.Add(actionUserGroups);

                AuditUnitOfWork.Save();
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }



        void AddDatabaseActions(List<Persistance.Models.Action> actions)
        {
            try
            {
                if (!AuditUnitOfWork.ActionRepository.Any(a => a.Code == DatabaseAuditActionsEnum.DatabaseAdd.ToString()))
                    actions.Add(new Persistance.Models.Action() { Code = DatabaseAuditActionsEnum.DatabaseAdd.ToString() });

                if (!AuditUnitOfWork.ActionRepository.Any(a => a.Code == DatabaseAuditActionsEnum.DatabaseUpdate.ToString()))
                    actions.Add(new Persistance.Models.Action() { Code = DatabaseAuditActionsEnum.DatabaseUpdate.ToString() });

                if (!AuditUnitOfWork.ActionRepository.Any(a => a.Code == DatabaseAuditActionsEnum.DatabaseDelete.ToString()))
                    actions.Add(new Persistance.Models.Action() { Code = DatabaseAuditActionsEnum.DatabaseDelete.ToString() });
            }
            catch (Exception ex)
            {
                ErrorLoger.Log(ex);
            }
        }



    }

}
