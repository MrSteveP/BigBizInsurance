using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class AuditTrailFilterDto : FilterVModel<AuditTrailDto>
    {
        public string UserName { set; get; }
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        public string Audit_ActionCode { set; get; }
        public string EntityName { set; get; }
    }
}
