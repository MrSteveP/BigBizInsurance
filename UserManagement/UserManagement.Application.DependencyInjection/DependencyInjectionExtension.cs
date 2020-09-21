using Common.Domain.Module;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserManagement.Application.Authorization;
using UserManagement.Application.Persistance;
using UserManagement.Application.Services.Abstraction;
using UserManagement.Application.Services.Concrete;
using UserManagement.Domain.Models;
using UserManagement.Persistance;

namespace UserManagement.Application.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void AddUserManagement(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            AddIdentity(services);

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                AuthorizationExtension.ConfigureSystemAuthorization(options);
            });

            AuthorizationExtension.AddAuthorizationPolicies(services);

            services.ConfigureApplicationCookie(
            options =>
            {
                options.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = new PathString("/Identity/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddScoped<IUserManagementUnitOfWork, UserManagementUnitOfWork>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IDashboardService, DashboardService>();
        }

        public static void AddUserManagementWebApi(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            AddIdentity(services);

         
            services.AddScoped<IUserManagementUnitOfWork, UserManagementUnitOfWork>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IDashboardService, DashboardService>();
        }

        private static void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                 config =>
                 {
                     var dictionaryGeneral = CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.Values;
                     config.SignIn.RequireConfirmedAccount = Convert.ToBoolean(dictionaryGeneral[CommonStaticValues.SystemSettingsData.AuthenticationGeneralApplication.KeysEnum.RequireConfirmedAccount]); ;

                     var dictionary = CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.Values;
                     bool AllowThrottleAuthentication = Convert.ToBoolean(dictionary[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.AllowThrottleAuthentication]);
                     int LockoutTime = Convert.ToInt32(dictionary[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.LockoutTime]);
                     int MaximumNumberOfAttempts = Convert.ToInt32(dictionary[CommonStaticValues.SystemSettingsData.AuthenticationThrottlingApplication.KeysEnum.MaximumNumberOfAttempts]);


                     if (AllowThrottleAuthentication)
                     {
                         config.Lockout = new LockoutOptions
                         {
                             DefaultLockoutTimeSpan = TimeSpan.FromMinutes(LockoutTime),
                             MaxFailedAccessAttempts = MaximumNumberOfAttempts
                         };
                     }
                 }).AddUserManager<UserManager<ApplicationUser>>()
                   .AddRoleManager<RoleManager<ApplicationRole>>()
                   .AddEntityFrameworkStores<ApplicationDbContext>()
                   .AddDefaultTokenProviders();

        }
    }
}
