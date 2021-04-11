using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MicrosoftLog_API;

public class Program
{
    public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                   
                    logging.AddConsole();
                logging.AddDebug();
                logging.AddAzureWebAppDiagnostics();
            })
            .ConfigureServices(services =>
            {
                services.Configure<AzureFileLoggerOptions>(options =>
                {
                    options.FileName = "my-azure-diagnostics-";
                    options.FileSizeLimit = 50 * 1024;
                    options.RetainedFileCountLimit = 5;
                });
            })
            .UseStartup<Startup>();
}