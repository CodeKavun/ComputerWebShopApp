namespace ComputerWebShopApp.Models.ViewModels.Cart
{
    public class CartIndexVM
    {
        public ComputerShopDomainLibrary.Cart Cart { get; set; } = default!;
        public string? ReturnUrl { get; set; }
    }
}
