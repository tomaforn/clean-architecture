using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Todolist.Application.Interfaces;
using Modules.Todolist.Infrastructure.Files;
using Modules.Todolist.Infrastructure.Persistence;

namespace Modules.Todolist.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTodolistInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<TodolistDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<TodolistDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(TodolistDbContext).Assembly.FullName)));
            }

            services.AddScoped<ITodolistDbContext>(provider => provider.GetService<TodolistDbContext>());            
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }
    }
}