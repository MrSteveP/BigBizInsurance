using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BigBizInsurance.Web.Areas.Identity.IdentityHostingStartup))]
namespace BigBizInsurance.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}