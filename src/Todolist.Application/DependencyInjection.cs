using AutoMapper;
using Common.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todolist.Application.Mappings;

namespace Modules.Todolist.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTodolistApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(c => c.AddProfile<TodoListMappingProfile>(), Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
