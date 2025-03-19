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
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "customers.db");
            options.UseSqlite($"Data Source={dbPath}");
        }
    }
}
