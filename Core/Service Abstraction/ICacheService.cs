using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface ICacheService
    {
        Task <string?> GetCacheValueAsync (string key);
        Task SetCacheValueAsync (string cachekey , object value , TimeSpan duration);
    }
}
