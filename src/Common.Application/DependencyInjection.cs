using AutoMapper;
using Common.Application.Behaviours;
using Common.Application.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Modules.Todolist.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommonApplication(this IServiceCollection services)
        {
            //var mapperConfig = new MapperConfiguration(cfg =>
            //{
                //cfg.AddProfile<MappingProfile>();
            //});

            //services.AddAutoMapper(typeof(MappingProfile));
            
            //mapperConfig, Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
