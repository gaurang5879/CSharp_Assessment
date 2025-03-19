using CustomerApp.Data;
using CustomerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Repository
{
    public class CustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository()
        {
            _context = new CustomerContext();
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            Console.WriteLine("Customer added.");
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name, string email, string phone)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                customer.Name = name;
                customer.Email = email;
                customer.PhoneNumber = phone;
                _context.SaveChanges();
                Console.WriteLine("Customer updated.");
            }
        }

        /// <summary>
        /// Delete customer with id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                Console.WriteLine("Customer deleted.");
            }
        }

        /// <summary>
        /// Delete all customers
        /// </summary>
        public void DeleteAllCustomers()
        {
            var allCustomers = _context.Customers.ToList();
            _context.Customers.RemoveRange(allCustomers);
            _context.SaveChanges();

            //Reset auto-increment counter
            _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Customers'");

            Console.WriteLine("All customers deleted.");
        }

    }
}
