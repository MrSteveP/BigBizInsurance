using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    internal class ActionUserGroupRepository : AuditRepository<ActionUserGroup>, IActionUserGroupRepository
    {
        AuditDbContext Context { get; }
        public ActionUserGroupRepository(AuditDbContext context) : base(context)
        {
            Context = context;
        }

        async Task<ActionUserGroup> IActionUserGroupRepository.GetByActionCode(string actionCode) =>
          await Context.Audit_ActionUserGroups
         .FirstOrDefaultAsync(a => a.Audit_ActionCode == actionCode);

        async Task<ActionUserGroup> IActionUserGroupRepository.GetByActionCode_EntityName(string actionCode, string EntityName) =>
           await Context.Audit_ActionUserGroups
           .FirstOrDefaultAsync(a => a.Audit_ActionCode == actionCode && a.EntityName != null && a.EntityName.Contains(EntityName));

    }
}
