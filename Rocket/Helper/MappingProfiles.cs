  using AutoMapper;
using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using Rocket.DTOS;

namespace Rocket.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            // d=> destination
            // s=> source  
            CreateMap<Product, ProductToReturnDto>()
             .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
             .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
             .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<AddressDto,Core.Entities.Identity.Address > ().ReverseMap();
          

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();

            CreateMap<AddressDto, Core.Entities.Identity.OrderAgreggate.Address>();

            CreateMap<Order, OrderToReturnDTO>().ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName)).
                ForMember(d=>d.DeliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>().
                ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId)).
                ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName)).
                ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl)).
                ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
