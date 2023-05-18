using AutoMapper;
using Mango.Service.ProductAPI.Models;
using Mango.Service.ProductAPI.Models.Dto;

namespace Mango.Service.ProductAPI
{
    public class MappingConfig
    {
        public static IMapper GetMapperConfiguration()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });

            return mapperConfig.CreateMapper();
        }
    }
}
