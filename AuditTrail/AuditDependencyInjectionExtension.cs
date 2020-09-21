using AuditTrailComponent.Persistance;
using AuditTrailComponent.Persistance.Repositories.Abstraction;
using AuditTrailComponent.Persistance.Repositories.Concrete;
using AuditTrailComponent.Persistance.UnitOfWork;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.Concrete;
using AuditTrailComponent.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace AuditTrailComponent
{
    public static class AuditDIExtension
    {
        public static void AddAuditTrail(this IServiceCollection services, string connectionString, List<AuditActionDto> auditActionDtos)
        {
            services.AddDbContext<AuditDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IAuditUnitOfWork, AuditUnitOfWork>();

            services.AddScoped<INotificationInternalService, NotificationService>();
            services.AddScoped<INotificationPublicService, NotificationService>();
            services.AddScoped<IAuditTrailService, AuditTrailService>();
            services.AddScoped<IErrorLogInternalService, ErrorLogService>();
            services.AddScoped<IErrorLogPublicService, ErrorLogService>();

            var sp = services.BuildServiceProvider().CreateScope().ServiceProvider;
            var dbContext = sp.GetService<AuditDbContext>().Database;
            dbContext.EnsureCreated();

            if (dbContext.CanConnect())
                sp.GetService<IAuditTrailService>().IntiateActions(auditActionDtos);
        }
    }
}
