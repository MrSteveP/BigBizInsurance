using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("ActionUserGroup", Schema = "audit")]
    public class ActionUserGroup
    {
        public int Id { set; get; }
        public string Audit_ActionCode { set; get; }
        [StringLength(20)]
        public string EntityName { set; get; }
        public string ToEmails { set; get; }
        public string Message { set; get; }
        public string Subject { set; get; }
        public string From { set; get; }
    }
}
