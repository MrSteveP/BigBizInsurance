using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.Repositories.Abstraction;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    internal class NotificationRepository : AuditRepository<Notification>, INotificationRepository
    {
        AuditDbContext Context { get; }
        public NotificationRepository(AuditDbContext context) : base(context)
        {
            Context = context;
        }
    }
}
