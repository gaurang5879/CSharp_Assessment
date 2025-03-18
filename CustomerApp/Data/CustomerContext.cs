using System;
using CustomerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Data
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=customers.db");
        }
    }
}
