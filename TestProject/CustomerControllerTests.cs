using CustomerApp.API.Controllers;
using CustomerApp.Data.Models;
using CustomerApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Mock<ICustomerRepository> _mockRepo;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _controller = new CustomerController(_mockRepo.Object);
        }

        [Test]
        public void GetCustomers_Should_ReturnListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerID = 1, Name = "Alice", Email = "alice@example.com", PhoneNumber = "1111111111" },
                new Customer { CustomerID = 2, Name = "Bob", Email = "bob@example.com", PhoneNumber = "2222222222" }
            };

            _mockRepo.Setup(repo => repo.GetCustomers()).Returns(customers);

            // Act
            var result = _controller.GetCustomers() as OkObjectResult;
            var returnedCustomers = result.Value as List<Customer>;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, returnedCustomers.Count);
        }

        [Test]
        public void AddCustomer_Should_ReturnCreatedResponse()
        {
            // Arrange
            var customer = new Customer { Name = "Charlie", Email = "charlie@example.com", PhoneNumber = "3333333333" };

            // Act
            var result = _controller.AddCustomer(customer) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(201, result.StatusCode);
        }

        [Test]
        public void UpdateCustomer_Should_ReturnNoContent_IfSuccessful()
        {
            // Arrange
            var customer = new Customer { CustomerID = 1, Name = "David", Email = "david@example.com", PhoneNumber = "5555555555" };
            _mockRepo.Setup(repo => repo.UpdateCustomer(customer.CustomerID, customer.Name, customer.Email, customer.PhoneNumber)).Returns(true);

            // Act
            var result = _controller.UpdateCustomer(customer.CustomerID, customer) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public void DeleteCustomer_Should_ReturnNoContent_IfSuccessful()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteCustomer(1)).Returns(true);

            // Act
            var result = _controller.DeleteCustomer(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
