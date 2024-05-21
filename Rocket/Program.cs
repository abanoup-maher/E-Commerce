using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rocket.Core.Entities.Identity;
using Rocket.Repository.Data;
using Rocket.Repository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rocket
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerfactory=services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context=services.GetRequiredService<RocketStoreDbContext>();
                await context.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(context, loggerfactory); 

                var identitycontext=services.GetRequiredService<AppIdentityDbContext>();
                await identitycontext.Database.MigrateAsync();

                var usermanager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(usermanager);

            }
            catch(Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex,ex.Message);
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
}
