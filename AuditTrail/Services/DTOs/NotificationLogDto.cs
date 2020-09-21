using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.Mapper;
using System;

namespace AuditTrailComponent.Services.DTOs
{
    public class NotificationLogDto
    {
        public int Id { set; get; }
        public int NotificationId { set; get; }
        public DateTime TryDate { set; get; }
        public bool SendSucceeded { set; get; }
        public string ErrorMessage { set; get; }

        public static implicit operator NotificationLogDto(NotificationLog notificationLog)
        {
            return NotificationLogMapper.MapToDto(notificationLog);
        }

        public static implicit operator NotificationLog(NotificationLogDto notificationLog)
        {
            return NotificationLogMapper.MapToModel(notificationLog);
        }

    }
}
