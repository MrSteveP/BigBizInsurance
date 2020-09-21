using Microsoft.EntityFrameworkCore;

namespace AuditTrailComponent.Services.DTOs
{
    public class DatabaseChangesDto
    {
        public object Entity { set; get; }
        public object OriginalValues { set; get; }
        public object CurrentValues { set; get; }
        public EntityState State { set; get; }
        public string UserName { set; get; }
    }
}
