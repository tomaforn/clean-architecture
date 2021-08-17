using System;

namespace API.Client
{
    public class TokenProvider
    {
        public string SubjectId;
        public DateTimeOffset Expiration;
        public string IdToken;
        public string AccessToken;
        public string RefreshToken;
        public DateTimeOffset RefreshAt;
    }
}
