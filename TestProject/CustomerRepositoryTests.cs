using CustomerApp.Models;
using CustomerApp.Repository;

namespace TestProject
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private CustomerRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new CustomerRepository();
            _repo.DeleteAllCustomers();
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
        public void AddCustomer_NullCustomer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _repo.AddCustomer(null));
        }

        [Test]
        public void UpdateCustomer_ShouldModifyExistingRecord()
        {
            var customer = new Customer { Name = "Initial Name", Email = "initial@example.com", PhoneNumber = "1111111111" };
            _repo.AddCustomer(customer);
            var savedCustomer = _repo.GetCustomers().First();

            bool updated = _repo.UpdateCustomer(savedCustomer.CustomerID, "Updated Name", "updated@example.com", "2222222222");
            var updatedCustomer = _repo.GetCustomers().First();

            Assert.IsTrue(updated);
            Assert.AreEqual("Updated Name", updatedCustomer.Name);
            Assert.AreEqual("updated@example.com", updatedCustomer.Email);
            Assert.AreEqual("2222222222", updatedCustomer.PhoneNumber);
        }

        [Test]
        public void UpdateCustomer_NonExistentId_ShouldReturnFalse()
        {
            bool updated = _repo.UpdateCustomer(999, "Name", "email@example.com", "1234567890");
            Assert.IsFalse(updated);
        }

        [Test]
        public void DeleteCustomer_ShouldDecreaseCount()
        {
            _repo.AddCustomer(new Customer { Name = "Delete User", Email = "delete@example.com", PhoneNumber = "3333333333" });
            var initialCount = _repo.GetCustomers().Count;

            var customer = _repo.GetCustomers().First();
            bool deleted = _repo.DeleteCustomer(customer.CustomerID);

            var newCount = _repo.GetCustomers().Count;
            Assert.IsTrue(deleted);
            Assert.AreEqual(initialCount - 1, newCount);
        }

        [Test]
        public void DeleteCustomer_NonExistentId_ShouldReturnFalse()
        {
            bool deleted = _repo.DeleteCustomer(999);
            Assert.IsFalse(deleted);
        }

        [Test]
        public void DeleteAllCustomers_ShouldRemoveAllRecords()
        {
            _repo.AddCustomer(new Customer { Name = "User1", Email = "user1@example.com", PhoneNumber = "4444444444" });
            _repo.AddCustomer(new Customer { Name = "User2", Email = "user2@example.com", PhoneNumber = "5555555555" });
            Assert.IsTrue(_repo.GetCustomers().Count > 0);

            _repo.DeleteAllCustomers();
            Assert.That(_repo.GetCustomers().Count, Is.EqualTo(0));
        }

        [Test]
        public void DeleteAllCustomers_EmptyDatabase_ShouldNotThrowError()
        {
            Assert.DoesNotThrow(() => _repo.DeleteAllCustomers());
            Assert.AreEqual(0, _repo.GetCustomers().Count);
        }
    }
}