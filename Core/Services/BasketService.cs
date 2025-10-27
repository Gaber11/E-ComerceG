using Domain.Exeptions;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepo _basketRepo, IMapper _mapper) : IBasketServices
    {
        private readonly IMapper mapper = _mapper;

        public async Task<bool> DeleteBasketAsync(string Id)
    => await _basketRepo.DeleteBasketAsync(Id);

        public async Task<BasketDto> GetBasketAsync(string Id)
        {
            var basket = await _basketRepo.GetBasketAsync(Id);
            return basket is null ? throw new BasketNotFoundExeption(Id) : _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = await _basketRepo.UpdateBasket(_mapper.Map<CustomerBasket>(basket));
            return customerBasket is null ? throw new Exception("Can not Update the basket now") : _mapper.Map<BasketDto>(customerBasket);
        }
    }
}
