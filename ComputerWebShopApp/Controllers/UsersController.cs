using AutoMapper;
using ComputerWebShopApp.Data;
using ComputerWebShopApp.Models.DTOs.Users;
using ComputerWebShopApp.Models.ViewModels.Claims;
using ComputerWebShopApp.Models.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComputerWebShopApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<ShopUser> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShopUser> users = await userManager.Users
                .ToListAsync();
            IEnumerable<ShopUserDTO> userDTOs = mapper
                .Map<IEnumerable<ShopUserDTO>>(users);
            return View(userDTOs);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return NotFound();
            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null)
                return NotFound("Користавача не знайдено!");
            ShopUserDTO userDTO = mapper.Map<ShopUserDTO>(shopUser);
            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShopUserDTO userDTO)
        {
            if (!ModelState.IsValid)
                return View(userDTO);
            ShopUser? user = await userManager.FindByIdAsync(userDTO.Id);
            if (user != null)
            {
                user.DateOfBirth = userDTO.DateOfBirth;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            else
                ModelState.AddModelError(string.Empty, "Користавача не знайдено!");
            return View(userDTO);
        }


        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null) return NotFound();
            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();
            ChangePasswordVM vM = new ChangePasswordVM()
            {
                Id = shopUser.Id,
                Email = shopUser.Email,
            };
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vM)
        {
            if (!ModelState.IsValid)
                return View(vM);
            ShopUser? user = await userManager.FindByIdAsync(vM.Id);
            if(user == null) return NotFound();
            var result = await userManager.ChangePasswordAsync(user, vM.OldPassword, vM.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(vM);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if(id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            ShopUserDTO dTO = mapper.Map<ShopUserDTO>(user);
            return View(dTO);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string? id)
        {
            if (id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowClaims(string? id)
        {
            if(id==null) return NotFound();
            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null) return NotFound();
            IList<Claim> claims = await userManager.GetClaimsAsync(shopUser);
            IndexClaimsVM vM = new IndexClaimsVM()
            {
                Claims = claims,
                UserName = shopUser.UserName!,
                Email = shopUser.Email!
            };
            return View("../Claims/Index",vM);
        }
    }
}
