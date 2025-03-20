using CustomerApp.Data.Models;

namespace CustomerApp.Data.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        List<Customer> GetCustomers();
        bool UpdateCustomer(int id, string name, string email, string phone);
        bool DeleteCustomer(int id);
    }
}
