using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Ticket.Application.Mappings;
using System.Reflection;

namespace Modules.Ticket.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTicketApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<TicketMappingProfile>(), Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
