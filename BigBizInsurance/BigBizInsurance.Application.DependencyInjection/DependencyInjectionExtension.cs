using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BigBizInsurance.Application.DTOs;
using BigBizInsurance.Application.Middleware;
using BigBizInsurance.Application.Persistance;
using BigBizInsurance.Application.Services.Abstraction;
using BigBizInsurance.Application.Services.Concrete;
using BigBizInsurance.Persistance;

namespace BigBizInsurance.Application.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void AddBigBizInsuranceModule(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<TenantInfo>(); // Adds a scoped tenant object, controlled via middleware (TenantInfoMiddleware)

            services.AddDbContext<BigBizInsuranceDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IBigBizInsuranceUnitOfWork, BigBizInsuranceUnitOfWork>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ISampleService, SampleService>();
         
        }

        public static void UseBigBizInsuranceTenant(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantInfoMiddleware>();
        }
    }
}
