using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddFilter("System", LogLevel.Warning)
                       .AddFilter("Microsoft", LogLevel.Warning)
                       .AddConfiguration(hostingContext.Configuration.GetSection("logging"))
                       .AddConsole()
                       .AddDebug()
                       .AddEventSourceLogger();
                // TODO how to use it?
                // .AddLog4Net("log4net.config", true);
            })
            .UseStartup<Startup>();
    }
}
