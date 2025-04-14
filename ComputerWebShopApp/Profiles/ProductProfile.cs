using AutoMapper;
using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.Products;

namespace ComputerWebShopApp.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() {
            CreateMap<Product, ProductDTO>().
                ReverseMap();
        }
    }
}
