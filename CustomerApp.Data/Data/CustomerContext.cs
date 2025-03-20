using CustomerApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Data.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
    }
}
