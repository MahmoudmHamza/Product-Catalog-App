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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get all products",
            Description = "Returns all products."
        )]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                if (products.Count == 0)
                {
                    return NoContent();
                }

                return Ok(products);
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
            Summary = "Get a product by ID",
            Description = "Returns a single product with the specified ID."
        )]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            try
            {
                var product = await _productService.GetAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
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
            Summary = "Create new product",
            Description = "Creates new product with the given parameters in the request body."
        )]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                var createdProduct = await _productService.CreateAsync(product);

                return Ok(createdProduct);
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
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, product);
                if (updatedProduct == null)
                {
                    return NotFound();
                } 

                return Ok(updatedProduct);
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
            Summary = "Delete a product by ID",
            Description = "Delete a single product with the specified ID."
        )]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var isDeleted = await _productService.DeleteAsync(id);
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
        
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get all products by category ID",
            Description = "Gets all products with the specified category ID."
        )]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var products = await _productService.GetAllProductsByCategoryId(categoryId);

                if (products == null)
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("{id}/ratings")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Add product rating",
            Description = "Adds rating for a product with the specified ID."
        )]
        public async Task<ActionResult<Product>> AddProductRating([FromRoute] int id, [FromBody] ProductRating rating)
        {
            try
            {
                var product = await _productService.GetAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _productService.AddProductNewRating(product, rating);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}/ratings/average")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Get product rating by ID",
            Description = "Get single product rating with the specified ID."
        )]
        public async Task<ActionResult<decimal>> GetProductAverageRating([FromRoute] int id)
        {
            try
            {
                var product = await _productService.GetAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product.Rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet("search")]
        [ProducesResponseType(typeof(Product), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [SwaggerOperation(
            Summary = "Search products by name",
            Description = "Gets all products with the given search name."
        )]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string query)
        {
            try
            {
                var products = await _productService.SearchProductsByName(query);
                if (products == null || products.Count == 0) 
                {
                    return NotFound();
                }
                
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}