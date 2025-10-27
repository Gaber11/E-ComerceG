using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositries
{
    public class BasketRepo(IConnectionMultiplexer connectionMultiplexer) : IBasketRepo
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task<bool> DeleteBasketAsync(string Id)
   => await _database.KeyDeleteAsync(Id);

        public async Task<CustomerBasket> GetBasketAsync(string Id)
        {
            var value = await _database.StringGetAsync(Id);
            if (value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket?>(value); // retrieve ==> C# object [Deserialize]


        }

        public async  Task<CustomerBasket> UpdateBasket(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket); // retrieve ==> Json [Serialize]
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(30));
            return isCreatedOrUpdated ?  await GetBasketAsync(basket.Id) : null;
        }
    }
}
