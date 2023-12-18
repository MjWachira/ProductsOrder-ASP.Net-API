using AutoMapper;
using ProductsOrder_ASP.Net_API.Models;
using ProductsOrder_ASP.Net_API.Models.Dtos;

namespace ProductsOrder_ASP.Net_API.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductDto, Products>().ReverseMap();
            CreateMap<AddOrderDto, Orders>();
            CreateMap<AddUserDto, Users>();
        }
    }
}
