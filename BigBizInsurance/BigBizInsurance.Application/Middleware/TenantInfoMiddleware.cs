using CrossCutting.Core.Extensions;
using Microsoft.AspNetCore.Http;
using BigBizInsurance.Application.DTOs;
using BigBizInsurance.Application.Extensions;
using System.Threading.Tasks;

namespace BigBizInsurance.Application.Middleware
{
    public class TenantInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantInfo = context.RequestServices.GetRequiredService<TenantInfo>();
            tenantInfo.TenantId = context.User.GetTenantId();

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

}
