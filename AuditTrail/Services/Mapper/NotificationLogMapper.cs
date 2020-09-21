using AuditTrailComponent.Persistance.Models;
using AuditTrailComponent.Services.DTOs;

namespace AuditTrailComponent.Services.Mapper
{
    public static class NotificationLogMapper
    {
        public static NotificationLogDto MapToDto(NotificationLog notificationLog)
        {
            return new NotificationLogDto
            {
                ErrorMessage = notificationLog.ErrorMessage,
                Id = notificationLog.Id,
                NotificationId = notificationLog.NotificationId,
                SendSucceeded = notificationLog.SendSucceeded,
                TryDate = notificationLog.TryDate
            };
        }

        public static NotificationLog MapToModel(NotificationLogDto notificationLogDto)
        {
            return new NotificationLog
            {
                ErrorMessage = notificationLogDto.ErrorMessage,
                Id = notificationLogDto.Id,
                NotificationId = notificationLogDto.NotificationId,
                SendSucceeded = notificationLogDto.SendSucceeded,
                TryDate = notificationLogDto.TryDate
            };
        }
    }
}
