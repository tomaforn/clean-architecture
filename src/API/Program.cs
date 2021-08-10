using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modules.Todolist.Infrastructure.Persistence;
using Modules.User.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await MigrateAndSeedDatabase(scope, services);
            }

            await host.RunAsync();
        }

        private static async Task MigrateAndSeedDatabase(IServiceScope scope, IServiceProvider services)
        {
            try
            {
                var todoContext = services.GetRequiredService<TodolistDbContext>();
                if (todoContext.Database.IsSqlServer())
                {
                    todoContext.Database.Migrate();
                    var userContext = services.GetRequiredService<UserDbContext>();
                    userContext.Database.Migrate();
                }
                await TodolistDbContextSeed.SeedSampleDataAsync(todoContext);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
