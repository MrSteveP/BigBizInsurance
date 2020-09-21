using Common.Application.DependencyInjection;
using CrossCutting.Core;
using CrossCutting.Core.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BigBizInsurance.Application.DependencyInjection;
using BigBizInsurance.Web.Code;
using System.Collections.Generic;
using System.Globalization;
using UserManagement.Application.DependencyInjection;
namespace BigBizInsurance.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddCommonModule(connectionString);
            services.AddUserManagement(connectionString);
            services.AddBigBizInsuranceModule(connectionString);
            services.AddHttpContextAccessor();

            #region Localization
            // Configure supported cultures and localization options
            services.AddCustomRequestLocalizationOptions();
            services.AddLocalization();
            #endregion

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddCustomHangFire(Configuration);
            services.AddCustomAuditTrail(Configuration);
            services.AddControllers();

            #region Allow larg request size
            //For application running on IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            //For application running on Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
            });

            //Form's MultipartBodyLengthLimit
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            #endregion 
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

          
            ApplicationLogging.ConfigureNlogLogger(loggerFactory);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBigBizInsuranceTenant();
            #region localization
            app.UseGlobalization();
            app.UseRouter(EmbeddedResourcesConfigRouter.RegisterGlobalizationRoutes(app));

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"), //English US
                new CultureInfo("ar-SA"), //Arabic SA
            };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"), //English US will be the default culture (for new visitors)
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);

            // var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            // app.UseRequestLocalization(locOptions.Value);
            #endregion

         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }
    }
}
