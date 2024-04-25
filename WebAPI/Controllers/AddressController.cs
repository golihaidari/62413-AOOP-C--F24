using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController(IRepository<Address> repository, IJwtService jwtService) : ControllerBase
    {

        // GET: api/<AddressController>
        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            var addresses = await repository.GetAll();
            if (addresses != default && addresses.Any())
            {
                return Ok(addresses);
            }

            return NotFound("There is no address exist!");
        }

        // GET api/<AddressController>/5
        [HttpGet("{email}")]
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<string>> Get(string email)
        {
            var address = await repository.Get(email);
            if (address != default)
            {
                return Ok(address);
            }

            return NotFound("The specified address does not exist!");
        }

        // POST api/<AddressController>
        [HttpPost]
        [Authorize(Roles ="Customer")]
        public async Task<ActionResult<string>> Register([FromBody] Address address)
        {
            var userEmail = jwtService.GetEmailFromToken(User);
            var userRole = jwtService.GetRoleFromToken(User);
            if (userRole != Roles.Customer.ToString() && userEmail != address.Email)
            {
                return BadRequest("Access denied!");
            }          

            if (await repository.Create(address))
            {
                return Created("shoppingApiServer", "Address is registered successfully.");
            }

            return NotFound("Address could not be registered!");
        }

        // PUT api/<AddressController>/5
        [HttpPut]
        [Authorize(Roles ="Customer")]
        public async Task<ActionResult<string>> Update([FromBody] Address address)
        {
            var _address = await repository.Get(address.Email.ToString());
            if (_address == default)
            {
                return NotFound("The specified address could not be found!");
            }

            if (await repository.Update(address))
            {
                return Ok("Address has been updated!");
            }

            return NotFound("Address cannot be updated!");
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{email}")]
        [Authorize(Roles ="Admin, Customer")]
        public async Task<ActionResult<string>> Delete(string email)
        {
            var address = await repository.Get(email);
            if (address == default)
            {
                return NotFound("The specified address could not be found!");
            }

            if (await repository.Delete(email))
            {
                return Ok("The specified address has been deleted!");
            }

            return NotFound("The specified address could not be deleted!");
        }
    
    }
}
