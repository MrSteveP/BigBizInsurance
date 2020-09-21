using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UserManagement.Domain.Models;

namespace UserManagement.Persistance
{
    public partial class ApplicationDbContext :  IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
  
        public DbSet<Gender> Genders { set; get; }
        public DbSet<ApplicationPolicy> ApplicationPolicies { set; get; }
        public DbSet<ApplicationPolicyRole> ApplicationPolicyRoles { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Gender>().Property(p => p.Id).UseIdentityColumn();

            modelBuilder.SeedGender()
                        .SeedDefaultRole()
                        .SeedDefaultUser()
                        .SeedDefaultUserRole()
                        .SeedDefaultPolicies();

            base.OnModelCreating(modelBuilder);
        }


    }
}
