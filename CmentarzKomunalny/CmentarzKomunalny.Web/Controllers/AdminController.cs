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
            string employeeStr = "EMPLOYEE";
          //  var employees = userManager.GetUsersInRoleAsync(employeeStr);
            var users = context.Users.ToList();

            List<RegisterViewModel> newList = new List<RegisterViewModel>();
            foreach(var user in users)
            {
                RegisterViewModel listItem = new RegisterViewModel();
                
                    listItem.Email = user.Email;
                    listItem.UserId = user.Id;
                    newList.Add(listItem);    
            }
            return View(newList);
        }

        [HttpPost]
        public async Task<IActionResult> BlockEmployee(RegisterViewModel model)
        {
          //  if (ModelState.IsValid)
            
            //  var allUsers = context.Users.ToList();
            string blockedEmployee = "BLOCKEDEMPLOYEE";
            string employee = "EMPLOYEE";
            var allUsers = await userManager.GetUsersInRoleAsync(employee);
            var viewModels = new List<RegisterViewModel>();
                
            // print all users having role "Employee"
            foreach (var user in allUsers)
            {
                var currentRoles = await userManager.GetRolesAsync(user);
                    //  viewModels.Add(new RegisterViewModel { Email = user.Email, });
                var userViewModel = new RegisterViewModel
                {
                        UserId = user.Id,
                        Email = user.Email,
                };

                if (await userManager.IsInRoleAsync(user, employee))
                    userViewModel.IsSelected = true;
                else
                    userViewModel.IsSelected = false;

                    
                if (userViewModel.IsSelected == true)
                {
                    var resultRemove = await userManager.RemoveFromRolesAsync(user, currentRoles);
                    var resultAdd = await userManager.AddToRoleAsync(user, blockedEmployee);

                    if (resultRemove.Succeeded)
                    {
                        if(resultAdd.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, blockedEmployee);
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                }
                viewModels.Add(userViewModel);
                
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult UnblockEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UnblockEmployee(RegisterViewModel model)
        {
            
            string blockedEmployee = "BLOCKEDEMPLOYEE";
            string employee = "EMPLOYEE";

            var roleUnlocked = await roleManager.FindByIdAsync(employee);
            var roleBlocked = await roleManager.FindByIdAsync(blockedEmployee);
            var user = await userManager.FindByEmailAsync(model.Email);

            var currentRoles = await userManager.GetRolesAsync(user);

            if (roleUnlocked != null)
            {
                await userManager.RemoveFromRolesAsync(user, currentRoles);
            }
            var users = userManager.Users;
            var allUsers = context.Users.ToList();
            if(ModelState.IsValid)
            {

            }

            return View(users);
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