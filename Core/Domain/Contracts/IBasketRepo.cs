using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepo
    {
        //Get Basket
        Task<CustomerBasket> GetBasketAsync(string Id);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string Id);
        // Update or Create Basket
        Task<CustomerBasket> UpdateBasket(CustomerBasket basket , TimeSpan ? timeToLive =null);

    }
}
