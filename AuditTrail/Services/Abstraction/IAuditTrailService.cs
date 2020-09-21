using AuditTrailComponent.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    public interface IAuditTrailService
    {
        void IntiateActions(List<AuditActionDto> auditActionDtos);
        /// <summary>
        /// Save spicific actions
        /// </summary>
        /// <param name="auditTrailActionsEnum"></param>
        /// <param name="actionData"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task SaveCustomActionsAuditTrailAsync(string actionCode, string actionData, string userName);
        /// <summary>
        /// Save database actions
        /// </summary>
        /// <returns></returns>
        Task SaveDbActionsAuditTrailAsync(List<DatabaseChangesDto> databaseChanges);

        Task<AuditTrailFilterDto> Search(AuditTrailFilterDto model);
        Task<AuditTrailDto> GetById(int id);
    }
}
