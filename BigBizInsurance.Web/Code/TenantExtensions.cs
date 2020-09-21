using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using BigBizInsurance.Application.Services.Abstraction;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace BigBizInsurance.Web.Code
{
    public static class TenantExtensions
    {
        public static async Task AddTenantClaim(this UserManager<ApplicationUser> usermanager, HttpContext context, string userName,int tenantId)
        {
            ApplicationUser user =await usermanager.FindByNameAsync(userName);
            var claim = (await usermanager.GetClaimsAsync(user)).FirstOrDefault(c => c.Type == BigBizInsurance.Application.Extensions.TenantExtensions.TenantSchoolId && !string.IsNullOrEmpty(c.Value));

            if (claim == null)
                await usermanager.AddClaimAsync(user, new Claim(type: BigBizInsurance.Application.Extensions.TenantExtensions.TenantSchoolId, value: tenantId.ToString()));
            else if (claim.Value != tenantId.ToString())
                await usermanager.ReplaceClaimAsync(user, claim, new Claim(type: BigBizInsurance.Application.Extensions.TenantExtensions.TenantSchoolId, value: tenantId.ToString()));

        }


    }
}
