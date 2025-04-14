using ComputerShopDomainLibrary;
using ComputerWebShopApp.Data;
using ComputerWebShopApp.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWebShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext context;

        public HomeController(ShopContext context
            )
        {
            this.context = context;
        }
        public async Task <IActionResult> Index(int page=1)
        {
            int itemsPerPage = 2;
            IQueryable<Product> products = context.Products
                .Include(t => t.Brand)
                .Include(t => t.Category)
                .Include(t => t.ProductImages);
            //Фільтри
            int productsCount  = products.Count();
            int totalPages = (int)Math.Ceiling((float)productsCount / itemsPerPage);
            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            HomeIndexVM vM = new HomeIndexVM()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Products = await products.ToListAsync()
            };
            return View(vM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            //Product? product = await context.Products
            //    .FindAsync(id);
            //if(product == null)
            //    return NotFound();
            //await context.Entry(product).Reference(t => t.Brand).LoadAsync();
            //await context.Entry(product).Collection(t => t.ProductImages).LoadAsync();
            Product? product = await context.Products
                .Include(t=>t.Brand)
                .Include(t => t.ProductImages)
                .FirstOrDefaultAsync(t=>t.Id == id);
            return View(product);
        }
    }
}
