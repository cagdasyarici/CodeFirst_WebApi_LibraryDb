using _14_4_CodeFirst_WebApi_LibraryDb.Entities;
using _14_4_CodeFirst_WebApi_LibraryDb.Security;
using _14_4_CodeFirst_WebApi_LibraryDb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private LibraryDBContext db;
        private IConfiguration configuration;
        private IUserService userService;
        public UserController(LibraryDBContext _db, IConfiguration _configuration, IUserService _userService)
        {
            db = _db;
            configuration = _configuration;
            userService = _userService;
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult AuthLogin(User user)
        {
            bool isUser = ControlUser(user.UserName,user.Password);
            if(isUser)
            {
                Token token = Security.TokenHandler.CreateToken(user, configuration);
                return Ok(token);
            }
            else { return Unauthorized(); }
        }

        private bool ControlUser(string userName, string password)
        {
            User user = db.Users.FirstOrDefault(x=>x.UserName == userName && x.Password == password);
            if(user == null)
            {
                return false;
            }
            return true;
        }
        [HttpGet,Authorize]
        public ActionResult<object> GetMe()
        {
            //Tokendaki username'e ulaşma
            //var username = User?.Identity.Name;

            //var username = User.FindFirstValue(ClaimTypes.Name);
            //var role = User.FindFirstValue(ClaimTypes.Role);
            //return Ok(new { username, role });




            var username = userService.GetMyName();
            return Ok(username);
        }
    }
}
