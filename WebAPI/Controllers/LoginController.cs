using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IJwtService jwtService, IAuthService authService, IRepository<User> repository) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] User user)
        {
            var _user = await repository.Get(user.Email);
            if (_user != null && authService.VerifyPassword(_user.Password, user.Password))
            {
                var token = jwtService.GenerateToken(_user);
                return Accepted(new { Token = token, UserRole = _user.Role });
            }

            return Unauthorized("Incorrect username or password!");
        }          

    }
}
