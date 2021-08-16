using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todolist.Application.Mappings;

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
