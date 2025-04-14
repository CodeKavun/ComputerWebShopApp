using AutoMapper;
using ComputerWebShopApp.Data;
using ComputerWebShopApp.Models.DTOs.Roles;
using ComputerWebShopApp.Models.DTOs.Users;
using ComputerWebShopApp.Models.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWebShopApp.Controllers
{
    [Authorize(Roles = "admin,manager")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager,
            UserManager<ShopUser> userManager,
            IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var roles =  await roleManager.Roles.ToListAsync();
            IEnumerable<RoleDTO> roleDTOs = mapper.Map<IEnumerable<RoleDTO>>(roles);
            return View(roleDTOs);
        }
       
        public IActionResult Create()  => View();

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if(string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError(string.Empty, "Роль не може бути пустою!");
                return View(model: roleName);
            }
            IdentityRole role = new IdentityRole { Name = roleName };
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
       
        public  async Task<ActionResult> UserList() {
            IEnumerable<ShopUser> users = await userManager.Users
                .ToListAsync();
            IEnumerable<ShopUserDTO> userDTOs = mapper
                .Map<IEnumerable<ShopUserDTO>>(users);
            return View(userDTOs);
        }


        public async Task<IActionResult> ChangeRoles(string? id)
        {
            if (id == null)
                return NotFound();
            ShopUser? shopUser = await userManager.FindByIdAsync(id);
            if (shopUser == null)
                return NotFound();
            var allRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(shopUser);
            ChangeRolesVM vM = new ChangeRolesVM()
            {
                Id = shopUser.Id,
                Email = shopUser.Email,
                AllRoles = allRoles,
                UserRoles = userRoles
            };
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRoles(ChangeRolesVM vM)
        {
            ShopUser? shopUser = await userManager.FindByIdAsync(vM.Id);
            if (shopUser == null) return NotFound();
            var allRoles = await roleManager.Roles.ToListAsync();

            var userRoles = await userManager.GetRolesAsync(shopUser);
            if (ModelState.IsValid)
            {
                var addedRoles = vM.Roles.Except(userRoles);
                var deletedRoles = userRoles.Except(vM.Roles);
                await userManager.AddToRolesAsync(shopUser, addedRoles);
                await userManager.RemoveFromRolesAsync(shopUser, deletedRoles);
                return RedirectToAction("Index");
            }
            vM.AllRoles = allRoles;
            vM.UserRoles = userRoles;
            vM.Email = shopUser.Email;
            return View(vM);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if(id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if(role == null) return NotFound();
            RoleDTO roleDTO = mapper.Map<RoleDTO>(role);
            return View(roleDTO);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
