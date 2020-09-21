using AuditTrailComponent.Persistance.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    internal interface INotificationInternalService
    {
        Task SaveNotificationsAsync(string actionCode, List<AuditTrail> auditList);
        Task SaveNotificationAsync(string actionCode);
        Task SaveNotificationAsync(string actionCode, string EntityName);
    }
}
