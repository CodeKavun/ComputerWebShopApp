using Microsoft.AspNetCore.Identity;

namespace ComputerWebShopApp.Data
{
    public class ShopUser : IdentityUser
    {
        public DateOnly DateOfBirth { get; set; }
    }
}
