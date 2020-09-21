using CrossCutting.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BigBizInsurance.Web.Code
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _logger = ApplicationLogging.CreateLogger<ExceptionMiddleware>() as ILogger<ExceptionMiddleware>; ;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                HandleExceptionAsync(httpContext, ex);
                // await _next(httpContext);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ILogger _logger = ApplicationLogging.CreateLogger<ExceptionMiddleware>() as ILogger<ExceptionMiddleware>;
            _logger.LogError($"Something went wrong: {exception}");

            context.Response.Redirect("/Error");
        }


    }

}
