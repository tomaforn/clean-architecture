using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todolist.Application.Mappings;

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
