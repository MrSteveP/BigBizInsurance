using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UserManagement.Application.Authorization.Models;

namespace UserManagement.Application.Authorization
{
    public class AuthorizationConfig : IDisposable, IAuthorizePolicies, IConfigureAuthorizaion
    {
        #region Constants
        [JsonIgnore]
        public const string AnonymousPolicy = "Anonymous";
        [JsonIgnore]
        private const string FileName = "authorization.json";
        #endregion

        #region Properties
        [JsonIgnore]
        private static string RootPath => Directory.GetCurrentDirectory();
        [JsonIgnore]
        public static string FilePath => Path.Combine(RootPath, FileName);

        #region Serializable Members
        public HashSet<SitePolicy> SitePolicies { get; set; }
        public HashSet<SitePage> SitePages { set; get; }
        #endregion

        #endregion

        #region Constructor
        private AuthorizationConfig()
        {

        }
        #endregion

        #region Fluent Methods

        /// <summary>
        /// Read Authorization configuration from authorization.json
        /// </summary>
        /// <returns></returns>
        public static AuthorizationConfig ReadAuthorizationConfig()
        {
            return JsonConvert.DeserializeObject<AuthorizationConfig>(File.ReadAllText(FilePath));
        }


        public static void WriteAuthorizationConfig(HashSet<SitePolicy> sitePolicies, HashSet<SitePage> sitePages)
        {
            AuthorizationConfig config = new AuthorizationConfig();
            config.SitePolicies = sitePolicies;
            config.SitePages = sitePages;

            var json = JsonConvert.SerializeObject(config);
            System.IO.File.WriteAllText(FilePath, json);
        }

        /// <summary>
        /// Add authorization policies
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public IDisposable AddAuthorizationPolicies(IServiceCollection services)
        {
            SitePolicies?.ToList().ForEach(sitePolicy =>
            {

                services.AddAuthorizationCore(option =>
                {
                    if (sitePolicy.Roles.Any())
                        option.AddPolicy(sitePolicy.Name, policy => policy.RequireRole(sitePolicy.Roles));
                });
            });
            return this;
        }

        /// <summary>
        /// Allow Anonymous access to all pages in folders or in areas 
        /// </summary>
        /// <param name="options">Razor Pages Options</param>
        public IConfigureAuthorizaion AllowAnonymousToPages(RazorPagesOptions options)
        {
            List<SitePage> anonymousPages = GetAllAnonymousPages();

            anonymousPages.ForEach(sitePage =>
             {
                 options.Conventions.AllowAnonymousToPage(sitePage.Path);
             });

            return this;
        }

      

        /// <summary>
        /// Authorize all Pages that exist in root or in root folders 
        /// </summary>
        /// <param name="options"></param>
        public IConfigureAuthorizaion AuthorizePages(RazorPagesOptions options)
        {
            List<SitePage> mainAreaPages =GetAuthorizedMainAreaPages();

            mainAreaPages.ForEach(sitePage =>
            {
                options.Conventions.AuthorizePage(sitePage.Path, sitePage.PolicyName);
            });

            return this;
        }

      

        /// <summary>
        /// Authorize all area pages that exisit in area or in nested folders
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IConfigureAuthorizaion AuthorizeAreaPages(RazorPagesOptions options)
        {
            List<SitePage> authorizedaAreasPages = GetAuthorizedAreaPages();

            authorizedaAreasPages.ForEach(sitePage =>
             {
                 options.Conventions.AuthorizeAreaPage(sitePage.AreaName, sitePage.Path, sitePage.PolicyName);
             });

            return this;
        }

       

        #endregion

        #region Helper Methods
        private List<SitePage> GetAllAnonymousPages() => SitePages?
              .Where(sitepage => !string.IsNullOrEmpty(sitepage.PolicyName) && sitepage.PolicyName == AnonymousPolicy)?
              .ToList();

     

        private List<SitePage> GetAuthorizedMainAreaPages() => SitePages?
                            .Where(sitePage => !string.IsNullOrEmpty(sitePage.PolicyName) && sitePage.PolicyName != AnonymousPolicy && string.IsNullOrEmpty(sitePage.AreaName))?
                            .ToList();


      
        private List<SitePage> GetAuthorizedAreaPages() => SitePages?
                 .Where(sitePage => !string.IsNullOrEmpty(sitePage.PolicyName) && sitePage.PolicyName != AnonymousPolicy && !string.IsNullOrEmpty(sitePage.AreaName))?
                 .ToList();

     

        #endregion

        #region Disposing

        bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }
        ~AuthorizationConfig()
        {
            Dispose(false);
        }
        #endregion
    }
}
