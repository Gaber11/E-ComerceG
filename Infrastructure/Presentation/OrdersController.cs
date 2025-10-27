using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Authorization;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{

    public class OrdersController(IServiceManager serviceManager) : ApiController
    {
        // 4 Methods
        [HttpPost]
        public async Task<ActionResult> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await serviceManager.OrderService.CreateOrderAsync(orderRequest, email);

            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManager.OrderService.GetAllOrdersByEmailAsync(email);
            return Ok(orders);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResult>> GetOrder(Guid id)
        {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            var deliveryMethods = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

    }
}