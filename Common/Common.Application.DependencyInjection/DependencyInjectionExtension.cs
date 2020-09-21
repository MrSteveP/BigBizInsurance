using Common.Application.Persistance;
using Common.Application.Services.Abstraction;
using Common.Application.Services.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Common.Persistance;

namespace Common.Application.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void AddCommonModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CommonDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ICommonUnitOfWork, CommonUnitOfWork>();
            services.AddScoped<ISystemSettingsService, SystemSettingsService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAttachmentTypeSettings, AttachmentTypeSettings>();
        }
    }
}
