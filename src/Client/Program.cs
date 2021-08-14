using API.Client;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExecutableClient
{
    class Program
    {
        private static async Task Main()
        {
            var apiUrl = ConfigurationManager.AppSettings["ApiUri"];
            var identityServerUri = ConfigurationManager.AppSettings["IdentityServer"];

            var tokenProvider = await GetToken(identityServerUri);

            var httpClient = new HttpClient() { BaseAddress = new Uri(apiUrl) };
            var weatherclient = new WeatherForecastClient(tokenProvider, httpClient);

            var weatherList = await weatherclient.GetAsync();
            foreach (var weather in weatherList)
                Console.WriteLine($"Date: {weather.Date}, Temperature: {weather.TemperatureC}");            
        }

        private static async Task<TokenProvider> GetToken(string identityServerUri)
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(identityServerUri);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }
            return new TokenProvider() { AccessToken = tokenResponse.AccessToken };
        }
    }
}
