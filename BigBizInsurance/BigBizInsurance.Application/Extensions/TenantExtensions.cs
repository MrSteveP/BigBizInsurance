using System;
using System.Security.Claims;

namespace BigBizInsurance.Application.Extensions
{
    public static class TenantExtensions
    {
        public static string TenantSchoolId = "TenantSchoolId";

        public static int GetTenantId(this ClaimsPrincipal Identity)
        {
            var claim = Identity.FindFirst(c => c.Type == TenantSchoolId && !string.IsNullOrEmpty(c.Value));
            if (claim == null || string.IsNullOrEmpty(claim.Value))
                return 0;

            return Convert.ToInt32(claim.Value);
        }
    }
}
