using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("Notification", Schema = "audit")]
    public class Notification
    {
        public int Id { set; get; }
        public string Audit_ActionCode { get; set; }
        public string ToEmails { set; get; }
        public string Subject { set; get; }
        public string From { set; get; }
        public string Message { set; get; }
        public bool Sent { set; get; }
        public DateTime Date { set; get; } = DateTime.Now;
    }
}
