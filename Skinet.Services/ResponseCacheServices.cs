using Microsoft.IdentityModel.Tokens;
using Skinet.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skinet.Services
{
    public class ResponseCacheServices : IResponseCacheServices
    {
        private readonly IDatabase _database;

        public ResponseCacheServices(IConnectionMultiplexer redis )
        {
            _database = redis.GetDatabase();
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeSpan)
        {
            if (response is null)
                return;

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponse = JsonSerializer.Serialize(response , options);

            await _database.StringSetAsync(cacheKey, serializedResponse , timeSpan);
        }

        public async Task<string> GetCacheResponseAsync(string cashekey)
        {
            var cacheResponse = await _database.StringGetAsync(cashekey);

            if (cacheResponse.IsNullOrEmpty)
                return null;

            return cacheResponse;
        }
    }
}
