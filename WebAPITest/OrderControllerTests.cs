using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WebAPI.Controllers;
using WebAPI.Dtos;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Security;
using WebAPI.Utilities;

namespace WebAPITest
{
    [TestFixture]
    public class OrderControllerTests
    {
        private OrderController _orderController;
        private Mock<IRepository<Order>> _mockRepository;
        private Mock<IJwtService> _mockJwtService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Order>>();
            _mockJwtService = new Mock<IJwtService>();
            _orderController = new OrderController(_mockRepository.Object, _mockJwtService.Object);
        }

        [Test]
        public async Task GetAll_Orders_Returns_Ok()
        {
            // Arrange
            var orders = new List<Order> { 
                new Order
                {
                    Id= Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e"),
                    OrderDate= DateTime.Now,
                    Status= Status.Finished,
                    CustomerEmail= "goli@gmail.com",
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Id = Guid.Parse("1d1d1d1d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "1"
                        },
                        new OrderDetail
                        {
                            Id = Guid.Parse("2d2d2d2d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "2"
                        }
                    }
                },
                new Order
                {
                    Id= Guid.Parse("2a2b2c2d-d9cb-469f-a165-70867728666e"),
                     OrderDate= DateTime.Now,
                    Status= Status.Finished,
                    CustomerEmail= "goli@gmail.com",
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Id = Guid.Parse("1d1d1d1d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "1"
                        },
                        new OrderDetail
                        {
                            Id = Guid.Parse("2d2d2d2d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "2"
                        }
                    }
                }
            };

            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(orders);

            // Act
            var response = await _orderController.Get();

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetOrders_Returns_Ok()
        {
            // Arrange
            var userEmail = "goli@gmail.com";
            var orders = new List<Order> {
                new Order
                {
                    Id= Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e"),
                    OrderDate= DateTime.Now,
                    Status= Status.Finished,
                    CustomerEmail= "goli@gmail.com",
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Id = Guid.Parse("1d1d1d1d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "1"
                        },
                        new OrderDetail
                        {
                            Id = Guid.Parse("2d2d2d2d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "2"
                        }
                    }
                },
                new Order
                {
                    Id= Guid.Parse("2a2b2c2d-d9cb-469f-a165-70867728666e"),
                     OrderDate= DateTime.Now,
                    Status= Status.Finished,
                    CustomerEmail= "goli@gmail.com",
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail
                        {
                            Id = Guid.Parse("1d1d1d1d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "1"
                        },
                        new OrderDetail
                        {
                            Id = Guid.Parse("2d2d2d2d-d9db-469f-a165-70867728666e"),
                            Quantity = 2,
                            OrderId = Guid.Parse("1a1b1c1d-d9cb-469f-a165-70867728555e") ,
                            ProductId= "2"
                        }
                    }
                }
            };
            _mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(orders);

            // Act
            var response = await _orderController.Get(userEmail);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Create_Order_Returns_Created()
        {
            // Arrange
            var orderDto = new OrderDto
            {
                CustomerEmail = "goli@gmail.com",
                OrderDetails = new List<OrderDetailDto>
                {
                    new OrderDetailDto { Quantity = 1, Product = new Product { Id = "1" } }
                }
            };
            _mockJwtService.Setup(jwt => jwt.GetEmailFromToken(It.IsAny<ClaimsPrincipal>())).Returns(orderDto.CustomerEmail);
            _mockJwtService.Setup(jwt => jwt.GetRoleFromToken(It.IsAny<ClaimsPrincipal>())).Returns(Roles.Customer.ToString());
            _mockRepository.Setup(repo => repo.Create(It.IsAny<Order>())).ReturnsAsync(true);

            // Act
            var response = await _orderController.Register(orderDto);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<CreatedResult>());
        }

        [Test]
        public async Task Update_Order_Returns_Ok()
        {
            // Arrange
            var existingOrder = new Order { Id = Guid.NewGuid(), CustomerEmail = "goli@gmail.com" };
            var updatedOrder = new Order { Id = existingOrder.Id, CustomerEmail = existingOrder.CustomerEmail };
            _mockRepository.Setup(repo => repo.Get(existingOrder.Id.ToString())).ReturnsAsync(existingOrder);
            _mockRepository.Setup(repo => repo.Update(It.IsAny<Order>())).ReturnsAsync(true);

            // Act
            var response = await _orderController.Update(updatedOrder);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Delete_Order_Returns_Ok()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var existingOrder = new Order { Id = Guid.Parse(orderId), CustomerEmail = "goli@gmail.com" };
            _mockRepository.Setup(repo => repo.Get(orderId)).ReturnsAsync(existingOrder);
            _mockRepository.Setup(repo => repo.Delete(orderId)).ReturnsAsync(true);

            // Act
            var reponse = await _orderController.Delete(orderId);

            // Assert
            Assert.That(reponse, Is.Not.Null);
            Assert.That(reponse.Result, Is.InstanceOf<OkObjectResult>());
        }
    }
}
