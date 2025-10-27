using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface IBasketServices
    {
        //Get Basket
        Task<BasketDto> GetBasketAsync(string Id);
        // Delete Basket
        Task<bool> DeleteBasketAsync(string Id);
        // Update or Create Basket
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
    }
}
