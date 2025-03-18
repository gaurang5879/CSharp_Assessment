using CustomerApp.Data;
using CustomerApp.Models;

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

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            Console.WriteLine("Customer added.");
        }

        public List<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

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
    }
}
