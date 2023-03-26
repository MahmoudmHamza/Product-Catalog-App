using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Services;

namespace ProductsCatalogApp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ProductController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [HttpGet]
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
        public ActionResult<IEnumerable<Product>> SearchProducts(string query)
        {
            try
            {
                var products = _productService.SearchProductsByName(query);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // [HttpGet("{productId}/recommendations")]
        // public ActionResult<IEnumerable<Product>> GetRecommendations(int productId)
        // {
        //     var orderItems = _orderRepository.GetOrderItemsByProduct(productId);
        //     var userIds = orderItems.Select(oi => oi.UserId).Distinct();
        //     
        //     var recommendedProducts = _productRepository.GetProductsByUserIds(userIds);
        //     
        //     return Ok(recommendedProducts);
        // }
    }
}