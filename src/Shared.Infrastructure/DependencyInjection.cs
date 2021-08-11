using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Interfaces;
using Shared.Infrastructure.Services;

namespace Shared.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDomainEventService, DomainEventService>();            
            services.AddTransient<IDateTime, DateTimeService>();
            return services;
        }
    }
}