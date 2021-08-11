using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.User.Application.Shared.Interfaces;
using Modules.User.Infrastructure.Identity;
using Modules.User.Infrastructure.Persistence;
using Shared.Application.Interfaces;
using Shared.Infrastructure.Identity;
using Shared.Infrastructure.Services;

namespace Modules.User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName)));
            }

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();

            services.AddScoped<IDomainEventService, DomainEventService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });


            return services;
        }
    }
}