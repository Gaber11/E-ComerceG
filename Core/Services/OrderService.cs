using Domain.Entities.OrderEntities;
using Domain.Exeptions;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = Domain.Entities.OrderEntities.Address;

namespace Services
{
    internal class OrderService(IMapper mapper, IBasketRepo basketRepo, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // Address  [ShippingAddress]
            var shippingAddress = mapper.Map<ShippingAddress>(request.ShipToAddress); //Address
                                                                                        //OrderItem ==> Basket [BasketId] ==> BasketItems ==> OrderItems
            var basket = await basketRepo.GetBasketAsync(request.BasketId) ?? throw new BasketNotFoundExeption(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetGenericRepo<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundExeption(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            //DeliveryMethod  [DeliveryMethodId]
            var deliveryMethod = await unitOfWork.GetGenericRepo<DeliveryMethod, int>()
                .GetByIdAsync(request.DeliveryMethodId) ?? throw new DeliveryMethodNotFoundExeption(request.DeliveryMethodId);
            //Subtotal
            var orderRepo = unitOfWork.GetGenericRepo<Order, Guid>();
            var existingOrder = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId));
            if ( existingOrder != null)
            {
                orderRepo.Delete(existingOrder);
            }

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //Create new Order
            var order = new Order(userEmail, shippingAddress, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);

            // Save to Db
            await orderRepo.AddAsync(order);
            await unitOfWork.SaveChangeAsync();
            // map , return 
            return mapper.Map<OrderResult>(order);

        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
         => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl)
             , item.Quantity, product.Price);

        public async Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail)
        {
            var orders = await unitOfWork.GetGenericRepo<Order, Guid>()
                .GetAllAsync(new OrderWithIncludeSpecifications(userEmail));
            return mapper.Map<IEnumerable<OrderResult>>(orders);
        }

  
        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetGenericRepo<Order, Guid>()
                .GetByIdAsync(new OrderWithIncludeSpecifications(id))
                ?? throw new OrderNotFoundExeption(id);
            return mapper.Map<OrderResult>(order);

        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var methods = await unitOfWork.GetGenericRepo<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResult>>(methods);


        }


    }
}
