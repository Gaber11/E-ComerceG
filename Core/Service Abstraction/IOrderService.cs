using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Abstraction
{
    public interface IOrderService
    {
        //Get order by Id ==> OrderResult (Guid id)
        Task <OrderResult> GetOrderByIdAsync(Guid id);
        //Get all orders for user By email ==> IEnumerable<OrderResult> (string userEmail)
        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);
        //Create order ==> OrderResult (OrderRequest req , string userEmail)
        Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);
        //Get all delivery methods ==> IEnumerable<DeliveryMethodResult>()
        Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync();

    }
}
