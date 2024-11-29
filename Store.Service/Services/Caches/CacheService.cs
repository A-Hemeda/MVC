using StackExchange.Redis;
using Store.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.Services.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        //get data :
        public async Task<string> GetCacheKeyAsync(string key)
        {
            var result = await _database.StringGetAsync(key);
            if (result.IsNullOrEmpty) return null;
            return result.ToString();
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return;
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var ser_response = JsonSerializer.Serialize(response);
            await _database.StringSetAsync(key, ser_response, expireTime);

        }
    }
}
