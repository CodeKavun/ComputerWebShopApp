using ComputerShopDomainLibrary;

namespace ComputerWebShopApp.Models.ViewModels.Home
{
    public class HomeIndexVM
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
