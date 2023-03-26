using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Services;
using System.Linq;

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
        public async Task<ActionResult<IEnumerable<Product>>> GetRecommendationsForUser(int id)
        {
            try
            {
                var recommendedProducts = await _userService.GetRecommendedProducts(id);
                if (recommendedProducts == null || recommendedProducts.Count == 0)
                {
                    return NotFound();
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