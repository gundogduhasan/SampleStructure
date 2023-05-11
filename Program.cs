using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace SampleStructure
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(formatter: new CompactJsonFormatter(), path: "Logs/log.txt", rollingInterval: RollingInterval.Day)
               .MinimumLevel.Information()
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .Enrich.WithProperty("AppName", "Bogforing")
               .Enrich.WithProperty("Environment", "development")
               .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}