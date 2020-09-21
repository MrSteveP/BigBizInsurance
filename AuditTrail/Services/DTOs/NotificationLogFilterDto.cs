using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class NotificationLogFilterDto : FilterVModel<NotificationLogDto>
    {
        public bool? SendSucceeded { set; get; }
        public string ErrorMessage { set; get; }
        public DateTime? FromTryDate { set; get; }
        public DateTime? ToTryDate { set; get; }

    }
}
