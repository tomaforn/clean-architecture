using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Equipment.Application.Mappings;
using System.Reflection;

namespace Modules.Equipment.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEquipmentApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<EquipmentMappingProfile>(), Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
