using System;
using System.Net.Http;
using System.Threading.Tasks;
using Example.Api.Client.CSharp.Contracts;

namespace Example.Api.Client.CSharp
{
    public static class ClientFactory
    {
        //public static ITodoItemsClient CreateTodoItemsClient(string baseUrl, HttpClient http, Func<Task<string>> retrieveAuthorizationToken)
        //{
        //    return new TodoItemsClient(baseUrl, http)
        //    {
        //        RetrieveAuthorizationToken = retrieveAuthorizationToken
        //    };
        //}
        //public static ITodoListsClient CreateTodoListsClient(string baseUrl, HttpClient http, Func<Task<string>> retrieveAuthorizationToken)
        //{
        //    return new TodoListsClient(baseUrl, http)
        //    {
        //        RetrieveAuthorizationToken = retrieveAuthorizationToken
        //    };
        //}
        //public static IWeatherForecastClient CreateWeatherForecastClient(string baseUrl, HttpClient http, Func<Task<string>> retrieveAuthorizationToken)
        //{
        //    return new WeatherForecastClient(baseUrl, http)
        //    {
        //        RetrieveAuthorizationToken = retrieveAuthorizationToken
        //    };
        //}
    }
}
