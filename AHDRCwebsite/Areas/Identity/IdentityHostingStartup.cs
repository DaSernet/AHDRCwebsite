using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AHDRCwebsite.Areas.Identity.IdentityHostingStartup))]

namespace AHDRCwebsite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}