using AutoMapper;
using ProductShop.API.Domain;
using ProductShop.API.Dtos;

namespace ProductShop.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

        }
    }
}
