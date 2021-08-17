using API.Client;
using BlazorUI.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Blazored.Toast;
using BlazorUI.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using IdentityModel.Client;
using System.Net.Http;

namespace BlazorUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                    .AddNewtonsoftJson();

            services.AddScoped<TokenProvider>();            
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            RegisterApiClients(services);

            services.AddServerSideBlazor();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpClient();
            services.AddSingleton<IDiscoveryCache>(sp =>
            {
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(
                    "https://localhost:5001",
                    () => factory.CreateClient());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";

                options.ClientId = "blazor";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.SaveTokens = true;

                options.Scope.Add("openid");
                options.Scope.Add("api1");
                options.Scope.Add("offline_access");

                options.GetClaimsFromUserInfoEndpoint = true;

                options.Events = new OpenIdConnectEvents
                {
                    OnAccessDenied = context =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect("/");
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddBlazoredToast();
            services.AddSingleton<BlazorServerAuthStateCache>();
            services.AddScoped<AuthenticationStateProvider, BlazorServerAuthState>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub()
                        .RequireAuthorization();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void RegisterApiClients(IServiceCollection services)
        {
            services.AddHttpClient<ITodoItemsClient, TodoItemsClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));
            services.AddHttpClient<ITodoListsClient, TodoListsClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));
            services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));

            //Funkar inte att loop-regga alla?
            /*services.AddHttpClient("api", (client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri"));
            });

            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            var interfaceTypes = allTypes.Where(type => type.IsInterface && type.Namespace.Equals("ApiClient") && type.Name.EndsWith("Client"));
            foreach (var interfaceType in interfaceTypes)
            {
                var implementations = allTypes.Where(p => interfaceType.IsAssignableFrom(p));
                implementations.ToList().ForEach(i => {
                    services.AddScoped(interfaceType, provider => {
                        var clientFactory = provider.GetRequiredService<IHttpClientFactory>();
                        var httpClient = clientFactory.CreateClient("api");
                        return Activator.CreateInstance(i, httpClient);
                    });
                });
            }*/
        }
    }
}
