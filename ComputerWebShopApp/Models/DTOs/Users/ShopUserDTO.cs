using System.ComponentModel.DataAnnotations;

namespace ComputerWebShopApp.Models.DTOs.Users
{
    public class ShopUserDTO
    {
        public string Id { get; set; } = default!;

        [Display(Name = "Логін")]
        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;
        [Display(Name = "Дата народження")]
        public DateOnly DateOfBirth { get; set; }
    }
}
