using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.Repositories.Abstraction;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    internal class NotificationLogRepository : AuditRepository<NotificationLog>, INotificationLogRepository
    {
        AuditDbContext Context { get; }
        public NotificationLogRepository(AuditDbContext context) : base(context)
        {
            this.Context = context;
        }
    }
}
