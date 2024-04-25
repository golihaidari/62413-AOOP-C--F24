using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IRepository<Product> repository) : ControllerBase
    {

        // GET: api/<ProductController>
        [HttpGet]
        //[Authorize(Roles ="Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product?>>> Get()
        {
            var products = await repository.GetAll();
            if (products != default && products.Any())
            {
                return Ok(products);
            }

            return NotFound("The specified products does not exist!");
        }

        // GET api/<ProductController>/5
        [HttpGet("{productId}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<ActionResult<Product>> Get(string productId)
        {
            var product = await repository.Get(productId);
            if (product != default)
            {
                return Ok(product);
            }

            return NotFound("The specified product does not exist!");
        }

        // POST api/<ProductController>
        [HttpPost]
        //[Authorize(Roles ="Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Register([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Id))
            {
                return BadRequest("Product ID is required to create the product!");
            }

            var _product = await repository.Get(product.Id);
            if (_product != default)
            {
                return BadRequest("This product id is already exist!");
            }

            if (await repository.Create(product))
            {
                return Created("shoppingApi", "the prodcut is registered successfully!");
            }

            return NotFound("Product could not be registered!");
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> Update([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Id))
            {
                return BadRequest("Product ID is required to update the product!");
            }

            var _product = await repository.Get(product.Id);
            if (_product == default)
            {
                return NotFound("Product does not exist!");
            }

            if (await repository.Update(product))
            {
                return Ok("Product is updated successfuly");
            }

            return BadRequest("Product could not be updated!");
        }
    

        // DELETE api/<ProductController>/5
        [HttpDelete("{productId}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<ActionResult<string>> Delete(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest("Product ID is required to delete the product!");
            }

            if (await repository.Delete(productId))
            {
                return Ok("Product has been deleted!");
            }

            return NotFound("Product could not be deleted!");
        }
    }
}
