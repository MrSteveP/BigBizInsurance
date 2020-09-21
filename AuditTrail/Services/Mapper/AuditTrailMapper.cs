using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.DTOs;

namespace AuditTrailComponent.Services.Mapper
{
    public static class AuditTrailMapper
    {
        public static AuditTrailDto MapToDto(AuditTrail auditTrail)
        {
            return new AuditTrailDto
            {
                ActionData = auditTrail.ActionData,
                Date = auditTrail.Date,
                DateString = auditTrail.Date.ToString(),
                Audit_ActionCode = auditTrail.Audit_ActionCode,
                EntityName = auditTrail.EntityName,
                Id = auditTrail.Id,
                UserName = auditTrail.UserName
            };
        }

        public static AuditTrail MapToModel(AuditTrailDto auditTrailDto)
        {
            return new AuditTrail
            {
                ActionData = auditTrailDto.ActionData,
                Date = auditTrailDto.Date,
                Audit_ActionCode = auditTrailDto.Audit_ActionCode,
                EntityName = auditTrailDto.EntityName,
                Id = auditTrailDto.Id,
                UserName = auditTrailDto.UserName
            };
        }
    }
}
