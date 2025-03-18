using CustomerApp.Models;
using CustomerApp.Repository;

namespace TestProject
{
    public class Tests
    {
        private CustomerRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new CustomerRepository();
        }

        [Test]
        public void AddCustomer_ShouldIncreaseCount()
        {
            var initialCount = _repo.GetCustomers().Count;
            _repo.AddCustomer(new Customer { Name = "Test User", Email = "test@example.com", PhoneNumber = "1234567890" });

            var newCount = _repo.GetCustomers().Count;
            Assert.AreEqual(initialCount + 1, newCount);
        }

        [Test]
        public void DeleteCustomer_ShouldDecreaseCount()
        {
            _repo.AddCustomer(new Customer { Name = "Temp User", Email = "temp@example.com", PhoneNumber = "1234567890" });
            var initialCount = _repo.GetCustomers().Count;

            var customer = _repo.GetCustomers().Last();
            _repo.DeleteCustomer(customer.CustomerID);

            var newCount = _repo.GetCustomers().Count;
            Assert.AreEqual(initialCount - 1, newCount);
        }
    }
}