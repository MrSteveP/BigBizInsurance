using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Persistance.Repositories.Abstraction;
using AuditTrailComponent.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AuditTrailComponent.Persistance.Repositories.Concrete
{
    internal class AuditTrailRepository : AuditRepository<AuditTrail>, IAuditTrailRepository
    {
        AuditDbContext Context { get; }
        public AuditTrailRepository(AuditDbContext context) : base(context)
        {
            this.Context = context;
        }

        List<AuditTrail> IAuditTrailRepository.AddToAuditTrailList(List<DatabaseChangesDto> databaseChanges, string actionCode, EntityState entityState, List<AuditTrail> auditTrails)
        {
            List<AuditTrail> localAuditTrails = new List<AuditTrail>();
            foreach (var change in databaseChanges.Where(a=>a.State== entityState))
            {
                var EntityName = change.Entity.GetType().Name;
                var originalObject = change.OriginalValues;
                var currentObject = change.CurrentValues;
                var userName = change.UserName;

                var obj = JsonConvert.SerializeObject(new { originalObject, currentObject });
                localAuditTrails.Add(new AuditTrail { EntityName = EntityName, Audit_ActionCode = actionCode, ActionData = obj, UserName= userName });
            }

            auditTrails.AddRange(localAuditTrails);
            return localAuditTrails;
        }
    }
}
