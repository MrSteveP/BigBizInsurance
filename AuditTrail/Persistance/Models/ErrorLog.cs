using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("ErrorLog", Schema = "audit")]
    public class ErrorLog
    {
        public int Id { set; get; }
        public DateTime Date { set; get; }
        public string StackTrace { set; get; }
        public string Message { set; get; }
    }
}
