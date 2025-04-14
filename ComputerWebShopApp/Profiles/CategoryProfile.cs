using AutoMapper;
using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.Categories;

namespace ComputerWebShopApp.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<Category, CategoryDTO>()
                .ReverseMap();
        }
    }
}
