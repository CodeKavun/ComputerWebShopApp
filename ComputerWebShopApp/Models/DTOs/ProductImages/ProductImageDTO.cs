using ComputerShopDomainLibrary;
using ComputerWebShopApp.Models.DTOs.Products;
using System.ComponentModel.DataAnnotations;

namespace ComputerWebShopApp.Models.DTOs.ProductImages
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        [Display(Name = "Фото")]
        public byte[] ImageData { get; set; } = default!;
        [Display(Name = "Товар")]
        public int ProductId { get; set; }
        [Display(Name = "Товар")]
        public ProductDTO? Product { get; set; } = default!;
    }
}
