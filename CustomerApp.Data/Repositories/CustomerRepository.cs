using CustomerApp.Data.Data;
using CustomerApp.Data.Models;

namespace CustomerApp.Data.Repositories
{
    /// <summary>
    /// Customer entity repository.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        /// <summary>
        /// Contructor of customer repository
        /// </summary>
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers() => _context.Customers.ToList();

        /// <summary>
        /// Updates customer details if the customer exists.
        /// </summary>
        /// <param name="id">Customer's id.</param>
        /// <param name="name">Customer's name.</param>
        /// <param name="email">Customer's email.</param>
        /// <param name="phone">Customer's phone.</param>
        /// <returns>true or false</returns>
        public bool UpdateCustomer(int id, string name, string email, string phone)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return false;

            customer.Name = name;
            customer.Email = email;
            customer.PhoneNumber = phone;
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Deletes a customer by ID if they exist.
        /// </summary>
        /// <param name="id">Customer's id.</param>
        /// <returns>true or false</returns>
        public bool DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }
    }
}
