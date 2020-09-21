using AuditTrailComponent.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace AuditTrailComponent.Persistance
{
    public class AuditDbContext : DbContext
    {
        public DbSet<AuditTrail> AuditTrails { set; get; }
        public DbSet<Action> Audit_Actions { set; get; }
        public DbSet<ActionUserGroup> Audit_ActionUserGroups { set; get; }
        public DbSet<Notification> Audit_Notifications { set; get; }
        public DbSet<NotificationLog> NotificationLogs { set; get; }
        public DbSet<ErrorLog> ErrorLogs { set; get; }
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

}
