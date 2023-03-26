
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductsCatalogApp.Controllers;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Services;

namespace ProductsCatalogApp.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> productServiceMock;
        private ProductController productController;

        [SetUp]
        public void Setup()
        {
            productServiceMock = new Mock<IProductService>();
            productController = new ProductController(productServiceMock.Object);
        }

        [Test]
        public async Task GetAllProducts_ReturnsOkResult()
        {
            // Arrange
            var products = new List<Product> { new Product(), new Product() };
            productServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await productController.GetProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetProductById_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var product = new Product { Id = 1 };
            productServiceMock.Setup(service => service.GetAsync(product.Id)).ReturnsAsync(product);

            // Act
            var result = await productController.GetProduct(product.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task CreateProduct_WithValidProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var product = new Product { Name = "Product 1" };
            productServiceMock.Setup(service => service.CreateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await productController.CreateProduct(product);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task UpdateProduct_WithValidProduct_ReturnsNoContentResult()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1" };
            productServiceMock.Setup(service => service.UpdateAsync(product.Id, product)).ReturnsAsync(product);

            // Act
            var result = await productController.UpdateProduct(product.Id, product);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteProduct_WithValidId_ReturnsNoContentResult()
        {
            // Arrange
            var productId = 1;
            productServiceMock.Setup(service => service.DeleteAsync(productId)).ReturnsAsync(true);

            // Act
            var result = await productController.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }
    }
}