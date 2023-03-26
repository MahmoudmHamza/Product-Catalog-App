using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductsCatalogApp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get all orders",
            Description = "Returns all orders."
        )]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderService.GetAllAsync();
                if (orders.Count == 0)
                {
                    return NoContent();
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get an order by ID",
            Description = "Returns a single order with the specified ID."
        )]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            try
            {
                var order = await _orderService.GetAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Product), 201)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Create new order",
            Description = "Creates new order with the given parameters in the request body."
        )]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            try
            {
                var createdOrder = await _orderService.CreateAsync(order);

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Update an order by ID",
            Description = "Updates a single order with the specified ID."
        )]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateAsync(id, order);
                if (updatedOrder == null)
                {
                    return NotFound();
                } 

                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Delete an order by ID",
            Description = "Delete a single order with the specified ID."
        )]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            try
            {
                var isDeleted = await _orderService.DeleteAsync(id);
                if (isDeleted == false)
                {
                    return NotFound();
                } 
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}