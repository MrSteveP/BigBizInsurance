using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.Repositories.Abstraction;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    public class ErrorLogRepository : AuditRepository<ErrorLog>, IErrorLogRepository
    {
        AuditDbContext Context { get; }
        public ErrorLogRepository(AuditDbContext context) : base(context)
        {
            Context = context;
        }
    }
}
