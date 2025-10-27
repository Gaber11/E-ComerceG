using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositries
{
    public class CacheRepo(IConnectionMultiplexer connectionMultiplexer) : ICacheRepo
    {
        private readonly IDatabase _database  = connectionMultiplexer.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var serializedObj = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, serializedObj, duration);
        }
    }
}
