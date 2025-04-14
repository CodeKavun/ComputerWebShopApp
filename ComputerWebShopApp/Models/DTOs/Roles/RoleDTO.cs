using System.ComponentModel.DataAnnotations;

namespace ComputerWebShopApp.Models.DTOs.Roles
{
    public class RoleDTO
    {
        public string Id { get; set; } = default!;
        [Display(Name ="Назва ролі")]
        public string Name { get; set; } = default!;

    }
}
