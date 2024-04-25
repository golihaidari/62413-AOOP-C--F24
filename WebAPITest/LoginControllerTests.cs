using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

namespace WebAPITest
{
    [TestFixture]
    public class LoginControllerTests
    {
        private LoginController _loginController;
        private Mock<IJwtService> _mockJwtService;
        private Mock<IAuthService> _mockAuthService;
        private Mock<IRepository<User>> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockJwtService = new Mock<IJwtService>();
            _mockAuthService = new Mock<IAuthService>();
            _mockRepository = new Mock<IRepository<User>>();
            _loginController = new LoginController(_mockJwtService.Object, _mockAuthService.Object, _mockRepository.Object);
        }

        [Test]
        public async Task Login_Returns_Token_If_Valid_Credentials()
        {
            // Arrange
            var user = new User { Email = "goli@gmail.com", Password = "password", Role= Roles.Admin };
            var existingUser = new User { Email = "goli@gmail.com", Password = "hashedpassword", Role = Roles.Admin };
            _mockRepository.Setup(repo => repo.Get(user.Email)).ReturnsAsync(existingUser);
            _mockAuthService.Setup(auth => auth.VerifyPassword(existingUser.Password, user.Password)).Returns(true);
            _mockJwtService.Setup(jwt => jwt.GenerateToken(existingUser)).Returns("testtoken");

            // Act
            var response = await _loginController.Login(user);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<AcceptedResult>());
            var acceptedResult = response.Result as AcceptedResult;
            var json = JsonConvert.SerializeObject(acceptedResult!.Value);
            var jObject = JObject.Parse(json);
            Assert.That(jObject["Token"]!.ToString(), Is.EqualTo("testtoken"));
            Assert.That(jObject["UserRole"]!.ToString(), Is.EqualTo("0")); 
        }

        [Test]
        public async Task Login_Returns_Unauthorized_If_Invalid_Credentials()
        {
            // Arrange
            var user = new User { Email = "goli@gmail.com", Password = "password" };
            var existingUser = new User { Email = "goli@gmail.com", Password = "hashedpassword" };
            _mockRepository.Setup(repo => repo.Get(user.Email)).ReturnsAsync(existingUser);
            _mockAuthService.Setup(auth => auth.VerifyPassword(existingUser.Password, user.Password)).Returns(false);

            // Act
            var response = await _loginController.Login(user);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<UnauthorizedObjectResult>());
        }

        [Test]
        public async Task Login_Returns_Unauthorized_If_User_Not_Found()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            _mockRepository.Setup(repo => repo.Get(user.Email)).ReturnsAsync((User)null);

            // Act
            var response = await _loginController.Login(user);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<UnauthorizedObjectResult>());
        }
    }
}
