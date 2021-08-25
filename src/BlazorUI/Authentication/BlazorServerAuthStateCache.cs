﻿using API.Client;
using System;
using System.Collections.Concurrent;

namespace BlazorUI.Authentication
{
    public class BlazorServerAuthStateCache
    {
        private ConcurrentDictionary<string, TokenProvider> Cache
            = new ConcurrentDictionary<string, TokenProvider>();

        public bool HasSubjectId(string subjectId)
            => Cache.ContainsKey(subjectId);

        public void Add(string subjectId, DateTimeOffset expiration, string idToken, string accessToken, string refreshToken, DateTimeOffset refreshAt)
        {
            System.Diagnostics.Debug.WriteLine($"Caching sid: {subjectId}");

            var data = new TokenProvider
            {
                SubjectId = subjectId,
                Expiration = expiration,
                IdToken = idToken,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshAt = refreshAt
            };
            Cache.AddOrUpdate(subjectId, data, (k, v) => data);
        }

        public TokenProvider Get(string subjectId)
        {
            Cache.TryGetValue(subjectId, out var data);
            return data;
        }

        public void Remove(string subjectId)
        {
            System.Diagnostics.Debug.WriteLine($"Removing sid: {subjectId}");
            Cache.TryRemove(subjectId, out _);
        }
    }
}
