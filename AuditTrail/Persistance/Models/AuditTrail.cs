using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("AuditTrail",Schema = "audit")]
    public class AuditTrail
    {
        public int Id { set; get; }
        [StringLength(30)]
        public string UserName { set; get; }
        public DateTime Date { set; get; } = DateTime.Now;
        public string Audit_ActionCode { set; get; }
        [StringLength(50)]
        public string EntityName { set; get; }
        public string ActionData { set; get; }
    }
}
