using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmentarzKomunalny.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CmentarzKomunalny.Web.Controllers
{
    public class AdminController : Controller
    {
        public SignInManager<IdentityUser> signInManager;
        public UserManager<IdentityUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        private readonly ILogger logger;
        public AdminController(SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, ILogger<AdminController> _logger)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            logger = _logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        public IEnumerable<SelectListItem> RolesList { get; set; }
        // making an employee
        [HttpGet]
        public IActionResult Register()
        {

            ViewBag.RoleUzytkownikow = new SelectList(roleManager.Roles, "Name", "Name");
            return View();
        }

        public string RolesListId { get; set; }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string roleId)
        {
            if (ModelState.IsValid)
            {
                var roles = roleManager.Roles;
                ViewBag.Roles = new SelectList(roles);
                ViewBag.roleId = roleId;



                var role = await roleManager.FindByIdAsync(roleId);
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleId);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.RoleUzytkownikow = new SelectList(roleManager.Roles, "Name", "Name");
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Rola o ID = {roleId} nie znaleziona";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                    userRoleViewModel.IsSelected = true;
                else
                    userRoleViewModel.IsSelected = false;

                model.Add(userRoleViewModel);
            }

            return View(model);

        }
    }
}