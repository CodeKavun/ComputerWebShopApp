using AutoMapper;
using ComputerWebShopApp.Models.DTOs.Roles;
using Microsoft.AspNetCore.Identity;

namespace ComputerWebShopApp.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() {
            CreateMap<IdentityRole, RoleDTO>().ReverseMap();
        }
    }
}
