using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Ticket.Application.Interfaces;
using Modules.Ticket.Infrastructure.Persistence;

namespace Modules.Ticket.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTicketInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<TicketDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<TicketDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(TicketDbContext).Assembly.FullName)));
            }

            services.AddScoped<ITicketDbContext>(provider => provider.GetService<TicketDbContext>());
            return services;
        }
    }
}