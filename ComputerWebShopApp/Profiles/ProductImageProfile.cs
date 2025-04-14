using AutoMapper;
using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.ProductImages;

namespace ComputerWebShopApp.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile() {
            CreateMap<ProductImage, ProductImageDTO>()
                .ReverseMap();
        }
    }
}
