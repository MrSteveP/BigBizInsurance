using AuditTrailComponent.Persistance.Models;
using System.Threading.Tasks;

namespace AuditTrailComponent.Persistance.Repositories.Abstraction
{
    internal interface IActionUserGroupRepository : IAuditRepository<ActionUserGroup>
    {
        Task<ActionUserGroup> GetByActionCode(string actionCode);
        Task<ActionUserGroup> GetByActionCode_EntityName(string actionCode, string EntityName);
    }
}
