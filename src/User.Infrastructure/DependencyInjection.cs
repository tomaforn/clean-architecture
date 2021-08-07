using Shared.Application.Interfaces;
using Common.Infrastructure.Identity;
using Common.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.User.Infrastructure.Identity;
using Models.User.Infrastructure.Persistence;
using Modules.User.Application.Shared.Interfaces;
using User.Infrastructure.Identity;

namespace Modles.User.Infrastructure
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

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
               .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
               .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
               .AddInMemoryClients(IdentityConfig.Clients)
               .AddAspNetIdentity<ApplicationUser>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication();
//                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });


            return services;
        }
    }
}