using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.Mapper;
using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class NotificationDto
    {
        public int Id { set; get; }
        public string Audit_ActionCode { get; set; }
        public string ToEmails { set; get; }
        public string Subject { set; get; }
        public string From { set; get; }
        public string Message { set; get; }
        public bool Sent { set; get; }
        public DateTime Date { set; get; } = DateTime.Now;

        public static implicit operator NotificationDto(Notification notification)
        {
            return NotificationMapper.MapToDto(notification);
        }

        public static implicit operator Notification(NotificationDto notification)
        {
            return NotificationMapper.MapToModel(notification);
        }
    }
}
