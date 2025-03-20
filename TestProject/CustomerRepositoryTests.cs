using CustomerApp.Data.Data;
using CustomerApp.Data.Models;
using CustomerApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private CustomerContext _context;
        private CustomerRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "TestCustomerDb") // Ensures a fresh DB for each test
                .Options;

            _context = new CustomerContext(options);
            _context.Database.EnsureDeleted(); // Reset DB before each test
            _context.Database.EnsureCreated();
            _repository = new CustomerRepository(_context);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose(); // Dispose context after test
        }

        [Test]
        public void AddCustomer_Should_AddCustomer_ToDatabase()
        {
            // Arrange
            var customer = new Customer { Name = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890" };

            // Act
            _repository.AddCustomer(customer);
            var customers = _repository.GetCustomers();

            // Assert
            Assert.AreEqual(1, customers.Count);
            Assert.AreEqual("John Doe", customers[0].Name);
        }

        [Test]
        public void GetCustomers_Should_ReturnAllCustomers()
        {
            // Arrange
            _repository.AddCustomer(new Customer { Name = "Alice", Email = "alice@example.com", PhoneNumber = "1111111111" });
            _repository.AddCustomer(new Customer { Name = "Bob", Email = "bob@example.com", PhoneNumber = "2222222222" });

            // Act
            var customers = _repository.GetCustomers();

            // Assert
            Assert.AreEqual(2, customers.Count);
        }

        [Test]
        public void UpdateCustomer_Should_UpdateExistingCustomer()
        {
            // Arrange
            var customer = new Customer { Name = "Charlie", Email = "charlie@example.com", PhoneNumber = "3333333333" };
            _repository.AddCustomer(customer);
            var existingCustomer = _repository.GetCustomers().First();

            // Act
            bool result = _repository.UpdateCustomer(existingCustomer.CustomerID, "Charlie Updated", "charlie.updated@example.com", "4444444444");
            var updatedCustomer = _repository.GetCustomers().First();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("Charlie Updated", updatedCustomer.Name);
        }

        [Test]
        public void DeleteCustomer_Should_RemoveCustomer()
        {
            // Arrange
            var customer = new Customer { Name = "David", Email = "david@example.com", PhoneNumber = "5555555555" };
            _repository.AddCustomer(customer);
            var existingCustomer = _repository.GetCustomers().First();

            // Act
            bool result = _repository.DeleteCustomer(existingCustomer.CustomerID);
            var customers = _repository.GetCustomers();

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, customers.Count);
        }
    }
}
