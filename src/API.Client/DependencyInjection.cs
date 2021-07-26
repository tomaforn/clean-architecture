using API.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<ITodoItemsClient,
                           TodoItemsClient>(client =>
             client.BaseAddress = new Uri(configuration.GetValue<Uri>("ApiUri")));


            return services;
        }
    }

}
