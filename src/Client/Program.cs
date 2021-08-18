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
            var todoListClient = new TodoListsClient(tokenProvider, httpClient);

            var todoList = await todoListClient.GetAsync();
            foreach (var list in todoList.Lists)
                Console.WriteLine($"Title: {list.Title}, Colour: {list.Colour}");
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
