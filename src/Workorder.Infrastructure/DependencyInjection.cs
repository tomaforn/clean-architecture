using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Workorder.Application.Interfaces;
using Modules.Workorder.Infrastructure.Persistence;

namespace Modules.Workorder.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkorderInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<WorkorderDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<WorkorderDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(WorkorderDbContext).Assembly.FullName)));
            }

            services.AddScoped<IWorkorderDbContext>(provider => provider.GetService<WorkorderDbContext>());
            return services;
        }
    }
}