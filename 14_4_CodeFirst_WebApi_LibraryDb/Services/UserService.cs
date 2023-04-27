using System.Security.Claims;

namespace _14_4_CodeFirst_WebApi_LibraryDb.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        public string GetMyName()
        {
            var result = string.Empty;
            if(httpContextAccessor != null)
            {
                result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;
        }
    }
}
