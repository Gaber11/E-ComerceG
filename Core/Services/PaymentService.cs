using Domain.Entities.OrderEntities;
using Domain.Exeptions;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using Stripe;
using Stripe.Forwarding;
using Product = Domain.Entities.Product;
namespace Services
{
    public class PaymentService(IBasketRepo basketRepo, IUnitOfWork unitOfWork,
        IMapper mapper, IConfiguration configuration) : IPaymentService
    {
        //1] set up stripe Api key [Secretkey]
        //2] Get the Basket 
        //3] Basket.Item.Price = Product.Price [Get the latest price from the product table]
        //4] Get deliverymethod and shipping price
        //5] Retrive deliverymethod [DB] and assign price of basket.ShippingPrice = deliverymethod.Shipping Price
        //6] totlal =subtotal + shippingprice [(item.price) * (item.quantity)]+ basket.shippingprice
        //7] Create or update paymentintent with stripe
        //8] Save changes to basket
        //9] Map and return basketDto
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];
            var basket = await basketRepo.GetBasketAsync(basketId) ?? throw new BasketNotFoundExeption(basketId);
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetGenericRepo<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundExeption(item.Id);
                item.Price = product.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method was selected");
            var deliveryMethod = await unitOfWork.GetGenericRepo<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundExeption(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Price;
            // Long ==> dollar ==> cent
            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + basket.ShippingPrice) * 100;
            var service = new PaymentIntentService();
            //If he want to create or update
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(createOptions); //Id , Clientsecret
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                var updatedIntent = await service.UpdateAsync(basket.PaymentIntentId, updateOptions);

                // حدّث الـ ClientSecret لو Stripe رجع واحد جديد
                basket.ClientSecret = updatedIntent.ClientSecret;
            }

            await basketRepo.UpdateBasket(basket);

            return mapper.Map<BasketDto>(basket);

        }

        public async Task UpdatePaymentStatus(string request, string stripeHeaders)
        {

            var endpointSecret = configuration.GetSection("StripeSettings")["EndPointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeaders, endpointSecret
                ,throwOnApiVersionMismatch:false);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            // Handle the event
            // If on SDK version < 46, use class Events instead of EventTypes
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentIntentSucceeded(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentIntentFailed(paymentIntent.Id);

            }
            // ... handle other event types
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }



        }

        private async Task UpdatePaymentIntentFailed(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetGenericRepo<Order, Guid>();
            var order = await orderRepo
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            order.PaymentStatus = OrderPaymentStatus.paymentReceived;
            orderRepo.Update(order);
            await unitOfWork.SaveChangeAsync();
        }

        private async Task UpdatePaymentIntentSucceeded(string paymentIntentId)
        {
            var orderRepo = unitOfWork.GetGenericRepo<Order, Guid>();
            var order = await orderRepo
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecifications(paymentIntentId));
            order.PaymentStatus = OrderPaymentStatus.paymentReceived;
            orderRepo.Update(order);
            await unitOfWork.SaveChangeAsync();

        }
    }
}
