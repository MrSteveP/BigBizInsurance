using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditTrailComponent.Persistance.Models
{
    [Table("Action", Schema = "audit")]
    public class Action
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public bool SendNotification { get; set; }
    }
}
