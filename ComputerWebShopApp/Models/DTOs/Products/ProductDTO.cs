using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.Brands;
using ComputerWebShopApp.Models.DTOs.Categories;
using System.ComponentModel.DataAnnotations;

namespace ComputerWebShopApp.Models.DTOs.Products
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Display(Name = "Назва товару")]
        public string ProductName { get; set; } = default!;
        [Display(Name = "Опис товару")]
        public string Description { get; set; } = default!;
        [Display(Name = "Вартість")]
        public double Price { get; set; }
        [Display(Name = "Виробник")]
        public int BrandId { get; set; }
        [Display(Name = "Виробник")]
        public BrandDTO? Brand { get; set; } = default!;
        [Display(Name = "Категорія")]
        public CategoryDTO? Category { get; set; } = default!;
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }

        public ICollection<ProductImage>? ProductImages { get; set; } = default!;
    }
}
