using API.Client;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorUI.Authentication
{
    public class BlazorServerAuthState : RevalidatingServerAuthenticationStateProvider
    {
        private readonly BlazorServerAuthStateCache Cache;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IDiscoveryCache OidcDiscoveryCache;

        public BlazorServerAuthState(
            ILoggerFactory loggerFactory,
            BlazorServerAuthStateCache cache,
            IHttpClientFactory httpClientFactory,
            IDiscoveryCache discoveryCache)
            : base(loggerFactory)
        {
            Cache = cache;
            this.httpClientFactory = httpClientFactory;
            OidcDiscoveryCache = discoveryCache;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(60*5); // TODO read from config

        protected async override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            var sid =
                authenticationState.User.Claims
                .Where(c => c.Type.Equals("sid"))
                .Select(c => c.Value)
                .FirstOrDefault();

            if (sid != null && Cache.HasSubjectId(sid))
            {
                var data = Cache.Get(sid);
                if (DateTimeOffset.UtcNow >= data.Expiration)
                {
                    Cache.Remove(sid);
                    return false;
                }
                if (data.RefreshAt < DateTimeOffset.UtcNow) await RefreshAccessToken(data);
            }
            return false;
        }
        private async Task RefreshAccessToken(TokenProvider data)
        {
            var client = httpClientFactory.CreateClient();
            var disco = await OidcDiscoveryCache.GetAsync();
            if (disco.IsError) return;

            var tokenResponse = await client.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "interactive.confidential.short",
                    ClientSecret = "secret",
                    RefreshToken = data.RefreshToken
                });
            if (tokenResponse.IsError) return;

            data.AccessToken = tokenResponse.AccessToken;
            data.RefreshToken = tokenResponse.RefreshToken;
            data.RefreshAt = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn / 2);
        }
    }
}
