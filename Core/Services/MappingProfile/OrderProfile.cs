using Domain.Entities.OrderEntities;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = Domain.Entities.OrderEntities.Address;
using Address = Domain.Entities.Address;
namespace Services.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod,DeliveryMethodResult>().ForMember(d=> d.Cost , options => options.MapFrom(s => s.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom(s => s.Product.PictureUrl));
            CreateMap<Order, OrderResult>()
                .ForMember(d => d.PaymentStatus, opt => opt.MapFrom(s => s.ToString()))
                .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, opt =>opt.MapFrom(s => s.Subtotal + s.DeliveryMethod.Price)).ReverseMap(); ;


        }
    }
}
