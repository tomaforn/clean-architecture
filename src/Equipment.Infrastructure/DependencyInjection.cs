using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Equipment.Application.Interfaces;
using Modules.Equipment.Infrastructure.Persistence;

namespace Modules.Equipment.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEquipmentInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<EquipmentDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<EquipmentDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(EquipmentDbContext).Assembly.FullName)));
            }

            services.AddScoped<IEquipmentDbContext>(provider => provider.GetService<EquipmentDbContext>());
            return services;
        }
    }
}