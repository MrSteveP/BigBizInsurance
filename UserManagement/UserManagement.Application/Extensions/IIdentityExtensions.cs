using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
namespace UserManagement.Application.Extensions
{
    public static class IIdentityExtensions
    {
        public static async Task<string> GetLastLoginDate(this IIdentity Identity, HttpContext httpContext)
        {
            if (Identity == null)
            {
                throw new ArgumentNullException(nameof(Identity));
            }

            if (!Identity.IsAuthenticated)
                return null;

            var userManager = httpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserAsync(httpContext.User);

            return user.LastLoginDate == null ? "" : user.LastLoginDate.Value.DateToString("dd/MM/yyyy hh:mm tt");
        }
    }
}
