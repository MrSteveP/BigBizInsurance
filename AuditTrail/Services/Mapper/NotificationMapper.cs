using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.DTOs;

namespace AuditTrailComponent.Services.Mapper
{
    public static class NotificationMapper
    {
        public static NotificationDto MapToDto(Notification notification)
        {
            return new NotificationDto
            {
                Audit_ActionCode = notification.Audit_ActionCode,
                Date = notification.Date,
                From = notification.From,
                Id = notification.Id,
                Message = notification.Message,
                Sent = notification.Sent,
                Subject = notification.Subject,
                ToEmails = notification.ToEmails
            };
        }

        public static Notification MapToModel(NotificationDto notification)
        {
            return new Notification
            {
                Audit_ActionCode = notification.Audit_ActionCode,
                Date = notification.Date,
                From = notification.From,
                Id = notification.Id,
                Message = notification.Message,
                Sent = notification.Sent,
                Subject = notification.Subject,
                ToEmails = notification.ToEmails
            };
        }
    }
}
