using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.Mapper;
using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class AuditTrailDto
    {
        public int Id { set; get; }

        public string UserName { set; get; }
        public DateTime Date { set; get; }
        public string DateString { set; get; }
        public string Audit_ActionCode { set; get; }
        public string EntityName { set; get; }
        public string ActionData { set; get; }

        public static implicit operator AuditTrailDto(AuditTrail auditTrail)
        {
            return AuditTrailMapper.MapToDto(auditTrail);
        }

        public static implicit operator AuditTrail(AuditTrailDto auditTrail)
        {
            return AuditTrailMapper.MapToModel(auditTrail);
        }
    }
}
