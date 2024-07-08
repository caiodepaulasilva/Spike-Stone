using Microsoft.EntityFrameworkCore;
using Infrastructure.Database;
using Spike_Stone.Controllers;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SQLDBContext>();
            db.Database.Migrate();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
}