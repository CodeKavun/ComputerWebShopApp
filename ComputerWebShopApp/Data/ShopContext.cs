using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using ComputerWebShopApp.Models.DTOs.Admin;
using ComputerWebShopApp.Models.DTOs.Users;
using ComputerWebShopApp.Models.ViewModels.Users;
using ComputerWebShopApp.Models.DTOs.Roles;

namespace ComputerWebShopApp.Data
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories  { get; set; }

        public DbSet<Product> Products  { get; set; }

        public DbSet<ProductImage> Images { get; set; }        
        public DbSet<ComputerWebShopApp.Models.DTOs.Roles.RoleDTO> RoleDTO { get; set; } = default!;
      
    }
}
