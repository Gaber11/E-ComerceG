using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepo cacheRepo) : ICacheService
    {
        public async Task<string?> GetCacheValueAsync(string cachekey)
     => await cacheRepo.GetAsync(cachekey);

        public async Task SetCacheValueAsync(string cachekey, object value, TimeSpan duration)
        => await cacheRepo.SetAsync(cachekey, value, duration);
    }
}
