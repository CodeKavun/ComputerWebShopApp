using System.ComponentModel.DataAnnotations;

namespace ComputerWebShopApp.Models.DTOs.Admin
{
    public class LoginUserDTO
    {
        [Required]
        [Display(Name = "Логін")]
        public string Username { get; set; } = default!;
        
        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Display(Name = "Залишатися в системі")]
        
        public bool RememberMe { get; set; }
    }
}
