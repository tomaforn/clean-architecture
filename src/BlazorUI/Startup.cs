using BlazorUI.API;
using BlazorUI.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            services.AddHttpClient<ITodoItemsClient,TodoItemsClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));
            services.AddHttpClient<ITodoListsClient, TodoListsClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));
            services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(client => client.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUri")));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
           .AddCookie("Cookies")
           .AddOpenIdConnect("oidc", options =>
           {
               //options.Authority = Configuration.GetValue<string>("ApiUri");
               //options.ClientId = "BlazorUI";
               //options.ClientSecret = "secret";
               options.Authority = "https://demo.identityserver.io/";
               options.ClientId = "interactive.confidential.short"; // 75 seconds
               options.ClientSecret = "secret";
               options.ResponseType = "code";
               options.SaveTokens = true;
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
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
