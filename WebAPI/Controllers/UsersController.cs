using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IRepository<User> repository, IAuthService authService) : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User?>>> Get()
        {
            var users = await repository.GetAll();
            if (users != null && users.Any())
            {
                return Ok(users);
            }

            return NotFound("There is no user exist!");
        }

        // GET api/<UsersController>/5
        [HttpGet("{Email}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<ActionResult<User>> Get(string Email)
        {           
            var user = await repository.Get(Email);
            if (user != default)
            {
                return Ok(user);
            }

            return NotFound("The specified user does not exist!");
        }

        // POST api/<UsersController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register([FromBody] User user)
        {            
            if (!authService.IsPasswordValid(user.Password))
            {
                return BadRequest("The password must have at least 12 letters and contain at least one upper case letter, one lower case letter, one number, and one special character!");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("User email is required to register the user!");
            }
            
            if (!authService.IsEmailValid(user.Email))
            {
                return BadRequest("Email format is not valid!");
            }

            var isEmailTaken = await repository.Get(user.Email);
            if (isEmailTaken != default)
            {
                return BadRequest("This email is already in use!");
            }

            user.Password = authService.HashPassword(user.Password);
            user.Role = Roles.Customer;

            if (await repository.Create(user))
            {
                return Ok("User is registered successfully!");
            }

            return NotFound("User could not be registered!");

        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<string>> Update([FromBody] User user)
        {
            if (!authService.IsPasswordValid(user.Password))
            {
                return BadRequest("The password must have at least 12 letters and contain at least one upper case letter, one lower case letter, one number, and one special character!");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("User email is required to register the user!");
            }

            if (!authService.IsEmailValid(user.Email))
            {
                return BadRequest("Email format is not valid!");
            }

            /*var userEmail = _jwtService.GetEmailFromToken(User);
            var userRole = _jwtService.GetRoleFromToken(User);

            if (userRole != Roles.Admin.ToString() && userEmail != user.Email)
            {
                return BadRequest("Access denied!");
            }*/

            var _user = await repository.Get(user.Email);
            if (_user == default)
            {
                return NotFound("User does not exist!");
            }

            _user.Password = authService.HashPassword(user.Password);

            if (await repository.Update(_user))
            {
                return Ok("User is updated successfully!");
            }
            else
            {
                return BadRequest("An error occurred!. User cannot be updated");
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{Email}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult<string>> Delete(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest("User email is required to delete the user account!");
            }

            var _user = await repository.Get(Email);
            if (_user == default)
            {
                return NotFound("User does not exist!");
            }

            if (await repository.Delete(Email))
            {
                return Ok("User has been deleted!");
            }

            return BadRequest("User could not be deleted!");
        }
    }
}
