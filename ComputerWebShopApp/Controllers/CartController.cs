﻿using ComputerShopDomainLibrary;
using ComputerWebShopApp.Data;
using ComputerWebShopApp.Extensions;
using ComputerWebShopApp.Models.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWebShopApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext context;
        string key = "cart";
        public CartController(ShopContext context)
        {
            this.context = context;
        }

        public IActionResult Index(Cart cart, string? returnUrl)
        {
            CartIndexVM vM = new CartIndexVM()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(vM);
        }

        public async Task<IActionResult> AddToCart(int? id, Cart cart, string? returnUrl)
        {
            if (id == null)
                return NotFound();
            Product? product = await context.Products
                .Include(t=>t.Brand)
                .Include(t=>t.ProductImages)
                .FirstOrDefaultAsync(t=>t.Id==id);
            if(product == null)
                return NotFound();
            //Cart cart = GetCart();
            cart.Add(new CartItem { Product = product, Count = 1 });
            //HttpContext.Session.Set(key, cart.CartItems);
            return RedirectToAction("Index", new {returnUrl });
        }
        [HttpPost]
        public IActionResult RemoveFromCart(Cart cart, int? id, string? returnUrl) { 
            if(id== null)
                return NotFound();
            cart.Remove(id.Value);
            //HttpContext.Session.Set(key, cart.CartItems);
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult Show()
        {
            string? userName = HttpContext.Session.GetString("CurrentUser");
            return View(model: userName);
        }

        public IActionResult SetUser()
        {
            HttpContext.Session.SetString("CurrentUser", "Сергій Рубан");

            return View();
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return View();
        }

        //public Cart GetCart()
        //{
        //    List<CartItem>? cartItems =
        //        HttpContext.Session.Get<List<CartItem>>(key);

        //    if (cartItems == null)
        //    {
        //        cartItems = new List<CartItem>();
        //        //HttpContext.Session.Set(key, cartItems);
        //    }
        //    Cart cart = new Cart(cartItems);
        //    return cart;
        //}
    }
}
