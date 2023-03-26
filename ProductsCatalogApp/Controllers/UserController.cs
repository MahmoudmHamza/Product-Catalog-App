using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductsCatalogApp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get all users",
            Description = "Returns all users."
        )]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                if (users.Count == 0)
                {
                    return NoContent();
                }

                return Ok(users);
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
            Summary = "Get a user by ID",
            Description = "Returns a single user with the specified ID."
        )]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                
                return Ok(user);
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
            Summary = "Create new user",
            Description = "Creates new user with the given parameters in the request body."
        )]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                var createdUser = await _userService.CreateAsync(user);
                return Ok(createdUser);
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
            Summary = "Update a product by ID",
            Description = "Updates a single product with the specified ID."
        )]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, user);
                if (updatedUser == null)
                {
                    return NotFound();
                } 

                return Ok(updatedUser);
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
            Summary = "Delete a user by ID",
            Description = "Delete a single user with the specified ID."
        )]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var isDeleted = await _userService.DeleteAsync(id);
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

        [HttpGet("{id}/recommendations")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get user recommended products by ID",
            Description = "Returns user's top 10 product recommendations based on his history and purchased items with the specified ID."
        )]
        public async Task<ActionResult<IEnumerable<Product>>> GetRecommendationsForUser(int id)
        {
            try
            {
                var recommendedProducts = await _userService.GetRecommendedProducts(id);
                if (recommendedProducts == null || recommendedProducts.Count == 0)
                {
                    return NoContent();
                }

                return Ok(recommendedProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}