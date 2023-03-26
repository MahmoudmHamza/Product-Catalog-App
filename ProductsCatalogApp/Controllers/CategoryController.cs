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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get all categories",
            Description = "Returns all categories."
        )]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                if (categories.Count == 0)
                {
                    return NoContent();
                }

                return Ok(categories);
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
            Summary = "Get a category by ID",
            Description = "Returns a single category with the specified ID."
        )]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            try
            {
                var category = await _categoryService.GetAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                
                return Ok(category);
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
            Summary = "Create new category",
            Description = "Creates new category with the given parameters in the request body."
        )]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            try
            {
                var createdCategory = await _categoryService.CreateAsync(category);
                return Ok(createdCategory);
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
            Summary = "Update a category by ID",
            Description = "Updates a single category with the specified ID."
        )]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] Category category)
        {
            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(id, category);
                if (updatedCategory == null)
                {
                    return NotFound();
                } 

                return Ok(updatedCategory);
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
            Summary = "Delete a category by ID",
            Description = "Delete a single category with the specified ID."
        )]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                var isDeleted = await _categoryService.DeleteAsync(id);
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