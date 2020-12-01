using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CmentarzKomunalny.Web.Helpers;

namespace CmentarzKomunalny.Web.Controllers
{
    public class UsersController : ControllerBase
    {
        [Authorize]
        [ApiController]
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
