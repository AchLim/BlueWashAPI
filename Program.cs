using Microsoft.EntityFrameworkCore;
using Serilog;
using WebAPI.Data;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
        try
        {
            Log.Information("Starting web application...");
            var host = CreateHostBuilder(args).UseSerilog().Build();
            var services = host.Services.GetService<IServiceScopeFactory>()!;


            using (var db = services.CreateScope().ServiceProvider.GetService<ApplicationContext>())
            {
                db!.Database.Migrate();
            }

            host.Run();
        }
        catch (System.Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                   .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
