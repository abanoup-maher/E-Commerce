using AutoMapper;
using Microsoft.Extensions.Configuration;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using Rocket.DTOS;

namespace Rocket.Helper
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return $"{_config["BaseApiUrl"]}{source.Product.PictureUrl}";
            }
            return null;
        }
    }
}
