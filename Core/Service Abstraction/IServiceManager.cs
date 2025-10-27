

using Domain.Contracts;

namespace Service_Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketServices BasketServices { get; }
        public IAuthenticationService authenticationService { get; }
        public IOrderService OrderService { get; }
        public IPaymentService PaymentService { get; }
        public ICacheService CacheService { get; }

    }
}
