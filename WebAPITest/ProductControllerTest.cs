using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPITest
{
    [TestFixture]
    public class ProductControllerTests
    {
        private ProductController _productController;
        private Mock<IRepository<Product>> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Product>>();
            _productController = new ProductController(_mockRepository.Object);
        }

        [Test]
        public async Task GetAll_Products_Returns_Ok()
        {
            // Arrange
            var products = new List<Product> { new Product() { Id = "1" }, new Product() { Id = "2" } };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            // Act
            var response = await _productController.Get();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = response.Result as OkObjectResult;
            Assert.That(products.Count, Is.EqualTo(((IEnumerable<Product>)okResult.Value).Count()));
        }

        [Test]
        public async Task Get_Product_Returns_Ok()
        {
            // Arrange
            var productId = "1";
            var product = new Product() { Id="1"};
            _mockRepository.Setup(repo => repo.Get(productId)).ReturnsAsync(product);

            // Act
            var response = await _productController.Get(productId);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = response.Result as OkObjectResult;
            Assert.That(okResult!.Value as Product, Is.EqualTo(product));
        }

        [Test]
        public async Task Create_Product_Returns_Created()
        {
            // Arrange
            var newProduct = new Product { Id = "1", Name = "Test Product", Price = 10.0m, Currency = "USD", RebateQuantity = 0, RebatePercent = 0 };
            _mockRepository.Setup(repo => repo.Get(newProduct.Id)).ReturnsAsync((Product)null);
            _mockRepository.Setup(repo => repo.Create(newProduct)).ReturnsAsync(true);

            // Act
            var response = await _productController.Register(newProduct);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public async Task Update_Product_Returns_Ok()
        {
            // Arrange
            var existingProduct = new Product { Id = "1", Name = "Existing Product", Price = 10.0m, Currency = "USD", RebateQuantity = 0, RebatePercent = 0 };
            var updatedProduct = new Product { Id = "1", Name = "Updated Product", Price = 20.0m, Currency = "USD", RebateQuantity = 0, RebatePercent = 0 };
            _mockRepository.Setup(repo => repo.Get(existingProduct.Id)).ReturnsAsync(existingProduct);
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Product>())).ReturnsAsync(true); // Using It.IsAny to match any Product argument

            // Act
            var response = await _productController.Update(updatedProduct);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Delete_Product_Returns_Ok()
        {
            // Arrange
            var productId = "1";
            _mockRepository.Setup(repo => repo.Delete(productId)).ReturnsAsync(true);

            // Act
            var response = await _productController.Delete(productId);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
