using AutoMapper;
using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.Brands;

namespace ComputerWebShopApp.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile() {
            CreateMap<Brand, BrandDTO>().
                ReverseMap();
        }
    }
}
