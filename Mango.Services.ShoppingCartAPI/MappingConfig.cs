using AutoMapper;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;

namespace Mango.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static IMapper GetMapperConfiguration()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<Cart, CartDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                //config.CreateMap<IEnumerable<CartDetails>, IEnumerable<CartDetailsDto>>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            });

            return mapperConfig.CreateMapper();
        }
    }
}
