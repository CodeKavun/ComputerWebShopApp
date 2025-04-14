using AutoMapper;
using ComputerWebShopApp.Data;
using ComputerWebShopApp.Models.DTOs.Users;

namespace ComputerWebShopApp.Profiles
{
    public class ShopUserProfile : RoleProfile
    {
        public ShopUserProfile() {
            CreateMap<ShopUser, ShopUserDTO>().ReverseMap();
        }
    }
}
