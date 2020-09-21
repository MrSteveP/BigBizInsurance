using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuditTrailComponent.Persistance.Repositories.Abstraction
{
    internal interface IAuditTrailRepository : IAuditRepository<AuditTrail>
    {
        List<AuditTrail> AddToAuditTrailList(List<DatabaseChangesDto> databaseChanges, string actionCode, EntityState entityState, List<AuditTrail> auditTrails);
    }
}
