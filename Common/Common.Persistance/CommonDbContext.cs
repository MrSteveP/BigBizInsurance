using Common.Domain.Module;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Common.Persistance
{
    public partial class CommonDbContext : DbContext
    {
      
        public CommonDbContext(DbContextOptions<CommonDbContext> options)
            : base(options)
        {
        }
  
        public DbSet<Attachment> Attachments { set; get; }
        public DbSet<AttachmentType> AttachmentTypes { set; get; }
        public DbSet<SystemSetting> SystemSettings { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Attachment>().Property(p => p.Id).UseIdentityColumn();
            modelBuilder.Entity<AttachmentType>().Property(p => p.Id).UseIdentityColumn();
            modelBuilder.Entity<SystemSetting>().Property(p => p.Id).UseIdentityColumn();

            modelBuilder.SeedSystemSettings()
                        .SeedAttachmentTypes();
            base.OnModelCreating(modelBuilder);
        }


    }
}
