using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContractWithWireMock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder().UseKestrel()
                 .UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration().UseStartup<Startup>()
                 .ConfigureAppConfiguration(ConfigureAppAction()).ConfigureLogging(ConfigureLogging()).Build();

            host.Run();
        }

        private static Action<WebHostBuilderContext, IConfigurationBuilder> ConfigureAppAction()
        {
            return (builderContext, config) =>
            {
                config.SetBasePath(builderContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", false, false)
                    .AddJsonFile("appsettings.Development.json", true, false).AddEnvironmentVariables()
                    .AddEnvironmentVariables();

                var configuration = config.Build();
            };
        }

        private static Action<WebHostBuilderContext, ILoggingBuilder> ConfigureLogging()
        {
            return (builderContext, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole(
                    options =>
                    {
                        options.IncludeScopes = Convert.ToBoolean(
                            builderContext.Configuration["Logging:IncludeScopes"]);
                    });
            };
        }
    }
}
