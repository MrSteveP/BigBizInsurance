// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalizationMiddleware.cs" company="Usama Nada">
//   No Copyright .. Copy, Share, and Evolve.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CrossCutting.Core.Globalization
{
    #region usings

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// The globalization middleware.
    /// </summary>
    public class GlobalizationMiddleware
    {
        /// <summary>
        /// The _next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalizationMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next.
        /// </param>
        public GlobalizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            
            //try
            //{
                CultureHelper.InitializeCultureFromCookie(context);
                await this.next.Invoke(context);
            //}
            //catch (System.Exception ex)
            //{

            //    throw ex;
            //}
           

            // Clean up.
        }
    }

    /// <summary>
    /// The globalization middleware extensions.
    /// </summary>
    public static class GlobalizationMiddlewareExtensions
    {
        /// <summary>
        /// The use globalization.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        /// <returns>
        /// The <see cref="IApplicationBuilder"/>.
        /// </returns>
        public static IApplicationBuilder UseGlobalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalizationMiddleware>();
        }
    }
}