using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebAPI.Dtos;
using WebAPI.Models;
using WebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Security;
using WebAPI.Utilities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IRepository<Order> repository) : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<Order?>>> Get()
        {
            var orders = await repository.GetAll();
            if (orders != null && orders.Any())
            {
                // Serialize the data using JsonSerializerOptions with ReferenceHandler.Preserve
                var jsonData = JsonSerializer.Serialize(orders, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return Ok(jsonData);
            }

            return NotFound("The specified orders does not exist!"); ;
        }

        // GET api/<OrderController>/5
        [HttpGet("{customerEmail}")]
        [Authorize(Roles ="Admin,Customer")]
        public async Task<ActionResult<IEnumerable<Order?>>> Get(string customerEmail)
        {
            var orders = await repository.GetAll();
            
            if (orders != default)
            {
                var filterList = orders.Where(o => o.CustomerEmail == customerEmail);
                if (filterList.Any())
                {
                    // Serialize the data using JsonSerializerOptions with ReferenceHandler.Preserve
                    var jsonData = JsonSerializer.Serialize(filterList, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });
                    return Ok(jsonData);
                }

                return NotFound("The specified order does not exist!");
            }

            return NotFound("The order does not exist!");
        }


        // POST api/<OrderController>
        [HttpPost]
        [Authorize(Roles ="Customer")]
        public async Task<ActionResult<string>> Register([FromBody] OrderDto orderDto)
        {          
            if (orderDto.OrderDetails.Count == 0)
            {
                return BadRequest("ProductDetails is required to register the order!");
            }

            
            Order order = new()
            {
                OrderDate = DateTime.Now,
                Status = Status.Pending,
                CustomerEmail = orderDto.CustomerEmail,
                OrderDetails = []
            };

            // Add order details to the order
            foreach (var orderDetailDTO in orderDto.OrderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = orderDetailDTO.Quantity,
                    OrderId = order.Id,
                    ProductId= orderDetailDTO.Product.Id                    
                };

                order.OrderDetails.Add(orderDetail);
            }
            order.Id = Guid.NewGuid();

            if (await repository.Create(order))
            {
                return Created("shoppingApi server", "Order is inserted successfully!");
            }

            return NotFound("Order could not be registered!");
        }

        

        // PUT api/<OrderController>/5
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> Update([FromBody] Order order)
        {
            var _order = await repository.Get(order.Id.ToString());
            if (_order == default)
            {
                return NotFound("Order does not exsist!");
            }

            order.OrderDate = DateTime.Now;
            if (await repository.Update(order))
            {
                return Ok("Order has been updated!");
            }

            return NotFound("Order could not be updated!");
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{orderId}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> Delete(string orderId)
        {
            var order = await repository.Get(orderId);
            if (order == default)
            {
                return NotFound("The specified address could not be found!");
            }

            if (await repository.Delete(orderId))
            {
                return Ok("Order has been deleted!");
            }

            return NotFound("Order could not be deleted!");
        }
    }
}
