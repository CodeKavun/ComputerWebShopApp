using ComputerWebShopApp.Data;
using ComputerWebShopApp.Models.DTOs.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerWebShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public AccountController(UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dTO)
        {
            if (!ModelState.IsValid)
                return View(dTO);
            ShopUser shopUser = new ShopUser
            {
                UserName = dTO.Username,
                Email = dTO.Email,
                DateOfBirth = dTO.DateOfBirth,
            };
            var result = await userManager.CreateAsync(shopUser, dTO.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(shopUser, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(dTO);
        }


        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO dTO)
        {
            if (!ModelState.IsValid)
                return View(dTO);
            ShopUser? shopUser = await userManager.FindByNameAsync(dTO.Username);
            if (shopUser != null)
            {
                var result = await signInManager.PasswordSignInAsync(shopUser, dTO.Password,
                    dTO.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(string.Empty, "Користувач/пароль не вірний");
            }
            else
                ModelState.AddModelError(string.Empty, "Користувача не знайдено");
            return View(dTO);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string? returnUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", returnUrl);
            return Challenge(properties, "Google");
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo? loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction("Login");
            var signInResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider,
                loginInfo.ProviderKey, isPersistent: false);
            if (!signInResult.Succeeded)
            {
                string[] userInfo =
                {
                loginInfo.Principal.FindFirst(ClaimTypes.Surname)!.Value!,
                loginInfo.Principal.FindFirst(ClaimTypes.Email)!.Value!
            };
                ShopUser? shopUser = await userManager.FindByEmailAsync(userInfo[1]);
                if (shopUser == null)
                {
                    shopUser = new ShopUser
                    {
                        Email = userInfo[1],
                        UserName = userInfo[1],
                    };
                    var createResult = await userManager.CreateAsync(shopUser);
                }
                var result = await userManager.AddLoginAsync(shopUser, loginInfo);
                await signInManager.SignInAsync(shopUser, isPersistent: false);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
