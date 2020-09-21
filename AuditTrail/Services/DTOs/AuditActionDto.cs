namespace AuditTrailComponent.Services.DTOs
{
    public class AuditActionDto
    {
        public string ActionCode { set; get; }
        public bool SendNotification { set; get; }
        public string NotificationEmails { set; get; }
        public string NotificationFromEmail { set; get; }
        public string NotificationSubject { set; get; }
        public string NotificationMessage { set; get; }
    }
}
