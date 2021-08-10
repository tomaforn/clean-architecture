﻿using ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ClientBase
    {
        private TokenProvider _tokenProvider;
        public ClientBase(TokenProvider provider)
        {
            _tokenProvider = provider;
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tokenProvider.AccessToken);
            return Task.FromResult(msg);
        }

    }
}
