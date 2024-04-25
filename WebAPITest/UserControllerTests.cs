using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

namespace WebAPITest
{
    [TestFixture]
    public class UserControllerTests
    {
        private UsersController _userController;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<IAuthService> _mockAuthService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<User>>();
            _mockAuthService = new Mock<IAuthService>();
            _userController = new UsersController(_mockRepository.Object, _mockAuthService.Object);
        }

        [Test]
        public async Task GetAll_Users_Returns_Ok()
        {
            // Arrange
            var users = new List<User> { new User() { Email = "goli@gmail.com", Password = "Password123#", Role = Roles.Customer },
                                         new User() { Email = "goli@gmail.com", Password = "Password123#", Role = Roles.Customer } };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(users);

            // Act
            var response = await _userController.Get();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = response.Result as OkObjectResult;
            Assert.That(users.Count, Is.EqualTo(((IEnumerable<User>)okResult.Value).Count()));
        }

        [Test]
        public async Task Get_User_Returns_Ok()
        {
            // Arrange
            var email = "goli@gmail.com";
            var user = new User() { Email = email };
            _mockRepository.Setup(repo => repo.Get(email)).ReturnsAsync(user);

            // Act
            var result = await _userController.Get(email);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult!.Value as User, Is.EqualTo(user));
        }

        [Test]
        public async Task Create_User_Returns_Ok()
        {
            // Arrange
            var newUser = new User { Email = "goli@gmail.com", Password = "Password123!", Role = Roles.Customer };
            _mockAuthService.Setup(auth => auth.IsPasswordValid(newUser.Password)).Returns(true);
            _mockAuthService.Setup(auth => auth.IsEmailValid(newUser.Email)).Returns(true);
            _mockRepository.Setup(repo => repo.Get(newUser.Email)).ReturnsAsync((User)null);
            _mockRepository.Setup(repo => repo.Create(newUser)).ReturnsAsync(true);

            // Act
            var result = await _userController.Register(newUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Update_User_Returns_Ok()
        {
            // Arrange
            var existingUser = new User { Email = "goli@gmail.com", Password = "Password123#", Role = Roles.Customer };
            var updatedUser = new User { Email = "goli@gmail.com", Password = "Password1234#", Role = Roles.Customer };
            _mockAuthService.Setup(auth => auth.IsPasswordValid(updatedUser.Password)).Returns(true);
            _mockAuthService.Setup(auth => auth.IsEmailValid(updatedUser.Email)).Returns(true);
            _mockRepository.Setup(repo => repo.Get(existingUser.Email)).ReturnsAsync(existingUser);
            _mockRepository.Setup(repo => repo.Update(existingUser)).ReturnsAsync(true); // Update with existingUser

            // Act
            var response = await _userController.Update(updatedUser);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Delete_User_Returns_Ok()
        {
            // Arrange
            var userEmail = "goli@gmail.com";
            _mockRepository.Setup(repo => repo.Get(userEmail)).ReturnsAsync(new User { Email = userEmail });
            _mockRepository.Setup(repo => repo.Delete(userEmail)).ReturnsAsync(true);

            // Act
            var response = await _userController.Delete(userEmail);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
