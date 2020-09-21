using AuditTrailComponent.Persistance.Repositories.Abstraction;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    internal class ActionRepository : AuditRepository<Models.Action>, IActionRepository
    {
        AuditDbContext Context { get; }
        public ActionRepository(AuditDbContext context) : base(context)
        {
            context = context;
        }
    }
}
