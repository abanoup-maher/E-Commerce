using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Rocket.Core.Entities;
using Rocket.DTOS;
using System.Security.Cryptography.X509Certificates;

namespace Rocket.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductPictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["BaseApiUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
