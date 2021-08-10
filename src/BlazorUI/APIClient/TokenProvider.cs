using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public class TokenProvider
    {
        public string AccessToken { get; set; }
    }
}
