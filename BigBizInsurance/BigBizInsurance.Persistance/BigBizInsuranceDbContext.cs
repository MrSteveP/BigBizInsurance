using Microsoft.EntityFrameworkCore;
using BigBizInsurance.Application.DTOs;
using BigBizInsurance.Domain.Models;
using System.Linq;
using Z.EntityFramework.Plus;

namespace BigBizInsurance.Persistance
{
    public class BigBizInsuranceDbContext : DbContext
    {

        public BigBizInsuranceDbContext(TenantInfo tenantInfo, DbContextOptions<BigBizInsuranceDbContext> options)
            : base(options)
        {
            this.Filter<TenantModel>(a => a.Where(f => f.Active == true));

            if (tenantInfo != null && tenantInfo.TenantId > 0)
            {
                this.Filter<SampleModel>(a => a.Where(f => f.Active == true && f.TenantId == tenantInfo.TenantId));
            }
            else
            {
                this.Filter<SampleModel>(a => a.Where(f => f.Active == true));
            }

        }


        public DbSet<SampleModel> Samples { set; get; }
        public DbSet<TenantModel> Tenants { set; get; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.SeedData();

            base.OnModelCreating(modelBuilder);
        }


    }
}
