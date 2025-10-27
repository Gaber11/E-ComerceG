using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepo
    {
        //Set [key , value , timeToLive(expiration Date)]
        Task SetAsync(string key, object value , TimeSpan duration);
        //Get
        Task<string?> GetAsync(string key);

    }
}
