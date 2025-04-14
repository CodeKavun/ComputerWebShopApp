using ComputerWebShopApp.Models.DTOs.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComputerWebShopApp.Models.ViewModels.Categories
{
    public class CreateCategoryVM
    {
        public CategoryDTO CategoryDTO { get; set; } = default!;

        public SelectList? ParentCategories  { get; set; }
    }
}
