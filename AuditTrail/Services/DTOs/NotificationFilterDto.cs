using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class NotificationFilterDto : FilterVModel<NotificationDto>
    {
        public string Audit_ActionCode { get; set; }
        public string ToEmails { set; get; }
        public string Subject { set; get; }
        public string From { set; get; }
        public string Message { set; get; }
        public bool? Sent { set; get; }
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }

    }
}
