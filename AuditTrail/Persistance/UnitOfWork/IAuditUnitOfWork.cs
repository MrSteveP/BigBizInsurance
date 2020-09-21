using AuditTrailComponent.Persistance.Repositories.Abstraction;
using System.Threading.Tasks;

namespace AuditTrailComponent.Persistance.UnitOfWork
{
    internal interface IAuditUnitOfWork
    {
        INotificationRepository NotificationRepository { get; }
        IActionUserGroupRepository ActionUserGroupRepository { get; }
        IActionRepository ActionRepository { get; }
        INotificationLogRepository NotificationLogRepository { get; }
        IAuditTrailRepository AuditTrailRepository { get; }
        IErrorLogRepository ErrorLogRepository { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
