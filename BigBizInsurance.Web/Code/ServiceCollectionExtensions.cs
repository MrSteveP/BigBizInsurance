using AuditTrailComponent;
using AuditTrailComponent.Services.Abstraction;
using AuditTrailComponent.Services.DTOs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BigBizInsurance.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BigBizInsurance.Web.Code
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomAuditTrail(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IEmailService, EmailService>();

            //AddAuditTrail classes
            services.AddAuditTrail(Configuration.GetConnectionString("DefaultConnection"),
            new List<AuditActionDto> {
new AuditActionDto() { ActionCode =BigBizInsuranceConstants.AuditActionsEnum.CreateSample.ToString()}});

            //register background job to Check Notifications to send emails
            var sp = services.BuildServiceProvider().CreateScope().ServiceProvider;
            var notificationService = sp.GetService<INotificationPublicService>();
            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("DefaultConnection"),
            new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            });
            RecurringJob.AddOrUpdate(() => notificationService.CheckNotificationsAndSend(), Cron.Daily);
        }

        public static void AddCustomHangFire(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            }));
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        public static void AddCustomRequestLocalizationOptions(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(
               options =>
               {
                   var supportedCultures = new[]
                                               {
                                                  new CultureInfo("ar"), new CultureInfo("en"),
                                                  new CultureInfo("en-GB"), new CultureInfo("ar-SA")
                                                };

                   // State what the default culture for your application is. This will be used if no specific culture
                   // can be determined for a given request.
                   options.DefaultRequestCulture = new RequestCulture("en-GB", "en-GB");

                   // You must explicitly state which cultures your application supports.
                   // These are the cultures the app supports for formatting numbers, dates, etc.
                   options.SupportedCultures = supportedCultures;

                   // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                   options.SupportedUICultures = supportedCultures;
               });
        }
    }
}
