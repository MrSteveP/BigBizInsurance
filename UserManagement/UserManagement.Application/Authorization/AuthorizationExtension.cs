using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Application.Authorization
{
    public class AuthorizationExtension
    {
        #region Public Methods
        public static void AddAuthorizationPolicies(IServiceCollection services)
        {
            AuthorizationConfig.ReadAuthorizationConfig()
                .AddAuthorizationPolicies(services)
                .Dispose();
        }

        public static void ConfigureSystemAuthorization(RazorPagesOptions options)
        {
            AuthorizationConfig.ReadAuthorizationConfig()
                .AuthorizeAreaPages(options)
                .AuthorizePages(options)
                .AllowAnonymousToPages(options)
                .Dispose();
        }

        

        #endregion

    }

}
