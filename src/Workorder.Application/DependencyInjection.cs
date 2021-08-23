using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Workorder.Application.Mappings;
using System.Reflection;

namespace Modules.Workorder.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWorkorderApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<WorkorderMappingProfile>(), Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
