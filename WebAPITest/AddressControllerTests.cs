using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

namespace WebAPITest
{
    [TestFixture]
    public class AddressControllerTests
    {
        private AddressController _addressController;
        private Mock<IRepository<Address>> _mockRepository;
        private Mock<IJwtService> _mockJwtService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Address>>();
            _mockJwtService = new Mock<IJwtService>();
            _addressController = new AddressController(_mockRepository.Object, _mockJwtService.Object);

        }

        [Test]
        public async Task GetAll_Addresses_Returns_Ok()
        {
            // Arrange
            var addresses = new List<Address> { new Address(), new Address() };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(addresses);

            // Act
            var response = await _addressController.Get();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = response.Result as OkObjectResult;
            Assert.That(addresses, Has.Count.EqualTo((okResult.Value as IEnumerable<Address>).Count()));
        }

        [Test]
        public async Task Get_Address_Returns_Ok()
        {
            // Arrange
            var email = "goli@gmail.com";
            var address = new Address();
            _mockRepository.Setup(repo => repo.Get(email)).ReturnsAsync(address);

            // Act
            var response = await _addressController.Get(email);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = response.Result as OkObjectResult;
            Assert.That(okResult!.Value as Address, Is.EqualTo(address));
        }

        [Test]
        public async Task Create_Address_Returns_Created()
        {
            // Arrange
            var newAddress = new Address { Email = "goli@gmail.com", FirstName = "Goli", LastName = "Haidari" };

            // Setup the mock to return a specific value for GetEmailFromToken and GetRoleFromToken methods
            _mockJwtService.Setup(jwt => jwt.GetEmailFromToken(It.IsAny<ClaimsPrincipal>())).Returns("goli@gmail.com");
            _mockJwtService.Setup(jwt => jwt.GetRoleFromToken(It.IsAny<ClaimsPrincipal>())).Returns(Roles.Customer.ToString());

            _mockRepository.Setup(repo => repo.Create(newAddress)).ReturnsAsync(true);

            // Act
            var result = await _addressController.Register(newAddress);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public async Task Update_Address_Returns_Ok()
        {
            // Arrange
            var existingAddress = new Address { Email = "goli@gmail.com", FirstName = "goli", LastName = "Hedari" };
            var updatedAddress = new Address { Email = "goli@gmail.com", FirstName = "Goli", LastName = "Haidari" };
            _mockRepository.Setup(repo => repo.Get(existingAddress.Email)).ReturnsAsync(existingAddress);
            _mockRepository.Setup(repo => repo.Update(updatedAddress)).ReturnsAsync(true);

            // Act
            var result = await _addressController.Update(updatedAddress);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Delete_Address_Returns_Ok()
        {
            // Arrange
            var email = "goli@gmail.com";
            _mockRepository.Setup(repo => repo.Get(email)).ReturnsAsync(new Address());
            _mockRepository.Setup(repo => repo.Delete(email)).ReturnsAsync(true);

            // Act
            var response = await _addressController.Delete(email);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
