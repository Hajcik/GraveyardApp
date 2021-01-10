using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CmentarzKomunalny.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using CmentarzKomunalny.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CmentarzKomunalny.Web.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    public class AdminController : Controller
    {
        public SignInManager<IdentityUser> signInManager;
        public UserManager<IdentityUser> userManager;
        public RoleManager<IdentityRole> roleManager;
        public IConfiguration configuration;
        private readonly ILogger logger;
        private readonly ApplicationDbContext context;
        public AdminController(ApplicationDbContext _context, IConfiguration _configuration, SignInManager<IdentityUser> _signInManager, UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager, ILogger<AdminController> _logger)
        {
            context = _context;
            configuration = _configuration;
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

        [HttpGet]
        public IActionResult BlockEmployee()
        {

            var users = context.Users.ToList();
            var userRoles = context.UserRoles.ToList();
            List<RegisterViewModel> newList = new List<RegisterViewModel>();
            foreach (var user in users)
            {
                foreach (var userRole in userRoles)
                {
                    RegisterViewModel listItem = new RegisterViewModel();

                    if (user.Id == userRole.UserId)
                    {
                        if (userRole.RoleId == "52538efa-ebb1-4ae9-b264-5ed1cc656056")
                        {
                            listItem.Email = user.Email;
                            listItem.UserId = user.Id;
                            newList.Add(listItem);
                        }
                    }
                }
            }
            return View(newList);
        }

        [HttpPost]
        public async Task<IActionResult> BlockEmployee(List<RegisterViewModel> model)
        {
            string blockedEmployee = "BLOCKEDEMPLOYEE";
            string employee = "EMPLOYEE";

            for (int i = 0; i < model.Count; i++)
            {
                model[i].Role = employee;
                var users = await userManager.GetUsersInRoleAsync(employee);
                var user = await userManager.FindByEmailAsync(model[i].Email);
                //var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                // 
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, blockedEmployee)))
                {
                    await userManager.RemoveFromRoleAsync(user, employee);
                    result = await userManager.AddToRoleAsync(user, blockedEmployee);
                }
                else if (model[i].IsSelected && model[i].Role == null)
                {
                    await userManager.AddToRoleAsync(user, blockedEmployee);
                }
                //  else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                //  {
                //      result2 = await userManager.RemoveFromRoleAsync(user, role.Name);
                //      // usunac go z roli, to ten if powinien trafic do unblock
                //  }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("BlockEmployee", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult UnblockEmployee()
        {

            var users = context.Users.ToList();
            var userRoles = context.UserRoles.ToList();
            List<RegisterViewModel> newList = new List<RegisterViewModel>();
            foreach (var user in users)
            {
                foreach (var userRole in userRoles)
                {
                    RegisterViewModel listItem = new RegisterViewModel();

                    if (user.Id == userRole.UserId)
                    {
                        if (userRole.RoleId == "a3cba8ab-6194-4b5f-b4c0-8bc2cb7776ba")
                        {
                            listItem.Email = user.Email;
                            listItem.UserId = user.Id;
                            newList.Add(listItem);
                        }
                    }
                }
            }
            return View(newList);
        }

        [HttpPost]
        public async Task<IActionResult> UnblockEmployee(List<RegisterViewModel> model)
        {
            string blockedEmployee = "BLOCKEDEMPLOYEE";
            string employee = "EMPLOYEE";

            for (int i = 0; i < model.Count; i++)
            {
                model[i].Role = blockedEmployee;
                var users = await userManager.GetUsersInRoleAsync(blockedEmployee);
                var user = await userManager.FindByEmailAsync(model[i].Email);
                //var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                // 
                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, employee)))
                {
                    await userManager.RemoveFromRoleAsync(user, blockedEmployee);
                    result = await userManager.AddToRoleAsync(user, employee);
                }
                else if (model[i].IsSelected && model[i].Role == null)
                {
                    await userManager.AddToRoleAsync(user, employee);
                }
                //  else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                //  {
                //      result2 = await userManager.RemoveFromRoleAsync(user, role.Name);
                //      // usunac go z roli, to ten if powinien trafic do unblock
                //  }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("UnblockEmployee", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
    
        // register employee
        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            var roles = context.Roles.ToList();
            var viewModel = new RegisterViewModel
            {
                Roles = roles,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(RegisterViewModel model, string roleId)
        {
            if (ModelState.IsValid)
            {
                var roles = roleManager.Roles;
                var role = await this.roleManager.FindByIdAsync(roleId);

                var user = new IdentityUser { UserName = model.Email, Email = model.Email,/* Role = model.Role*/ };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                 // nie potrzebujemy by system logował od razu
                 //   await signInManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "EMPLOYEE");
                    return RedirectToAction("Index", "Admin");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // register admin
        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            var roles = context.Roles.ToList();
            var viewModel = new RegisterViewModel
            {
                Roles = roles,
            };

            return View(viewModel);
        }

        public string RolesListId { get; set; }

       
        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model, string roleId)
        {
            
            if (ModelState.IsValid)
            {
                var roles = roleManager.Roles;
                var role = await this.roleManager.FindByIdAsync(roleId);
                
                var user = new IdentityUser { UserName = model.Email, Email = model.Email,/* Role = model.Role*/ };
                var result = await userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "ADMINISTRATOR");
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
        public IActionResult ListUsers()
        {
            var users = userManager.Users;

            return View(users);
        }

   /*     [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if(ModelState.IsValid)
            {
                if(id == null)
                {
                    return View("NotFound");
                }

                var user = await userManager.FindByIdAsync(id);
                var userNames = user.UserName;
                var rolesForUser = await userManager.GetRolesAsync(user);

                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var userName in userNames)
                    {
                       // ...
                    }
                }
            }

            return View();
        }
   */
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