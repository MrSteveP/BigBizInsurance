using AuditTrailComponent.Services.DTOs;
using System.Threading.Tasks;

namespace AuditTrailComponent.Services.Abstraction
{
    public interface INotificationPublicService
    {
        Task CheckNotificationsAndSend();
        Task<NotificationLogFilterDto> SearchNotificationLogs(NotificationLogFilterDto model);
        Task<NotificationFilterDto> SearchNotifications(NotificationFilterDto model);
    }
}
