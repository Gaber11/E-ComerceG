


using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServices> _basketServices;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICacheService> _cacheService;




        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IBasketRepo basketRepo, 
            UserManager<User> userManager, 
            IOptions<JwtOptions> options , IConfiguration configuration, ICacheRepo cacheRepo)
        {
            _cacheService = new Lazy<ICacheService>(() => new CacheService(cacheRepo));
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketServices = new Lazy<IBasketServices>(() => new BasketService(basketRepo, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,mapper, options));
            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepo, unitOfWork));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentService(basketRepo, unitOfWork, mapper, configuration));

        }

        public IProductService ProductService => _productService.Value;

        public IBasketServices BasketServices => _basketServices.Value;

        public IAuthenticationService authenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;

        public IPaymentService PaymentService => _paymentService.Value;

        public ICacheService CacheService => _cacheService.Value;
    }
}
