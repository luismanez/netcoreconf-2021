using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace Dalila.Haircare.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration(config =>
                        {
                            var settings = config.Build();
                            var connection = settings.GetConnectionString("AppConfig");

                            config.AddAzureAppConfiguration(options =>
                            {
                                options.Connect(connection);
                                options.Select(KeyFilter.Any);
                                options.UseFeatureFlags();
                            });
                        })
                        .UseStartup<Startup>();
                });
    }
}
