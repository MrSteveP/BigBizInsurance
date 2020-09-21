using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("NotificationLog", Schema = "audit")]
    public class NotificationLog
    {
        public int Id { set; get; }
        public int NotificationId { set; get; }
        public DateTime TryDate { set; get; }
        public bool SendSucceeded { set; get; }
        public string ErrorMessage { set; get; }
    }
}
