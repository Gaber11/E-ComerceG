using Microsoft.AspNetCore.Authorization;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize] // Login
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet] //Get: baseUrl/api/basket/id
        public async Task<ActionResult> Get(string id)
        {
            var basket = await _serviceManager.BasketServices.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost] //post: baseUrl/api/basket
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _serviceManager.BasketServices.UpdateBasketAsync(basketDto);
            return Ok(basket); //200
        }
        [HttpDelete("{id}")] // delete: baseUrl/api/basket/id
        public async Task<ActionResult> Delete(string id)
        {
            await _serviceManager.BasketServices.DeleteBasketAsync(id);
            return NoContent(); //204
        }
    }
}
