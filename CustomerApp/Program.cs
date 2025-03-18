using CustomerApp.Models;
using CustomerApp.Repository;

CustomerRepository repo = new();

//Adding new customers
repo.AddCustomer(new Customer { Name = "Test1", Email = "test1@test.com", PhoneNumber = "123456789" });
repo.AddCustomer(new Customer { Name = "Test2", Email = "test2@test.com", PhoneNumber = "234567891" });
repo.AddCustomer(new Customer { Name = "Test3", Email = "test3@test.com", PhoneNumber = "345678912" });

//Getting all customers
var customers = repo.GetCustomers();

//printing customers
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}");
}

//updating a customer
repo.UpdateCustomer(1, "test updated", "updatedTest@test.com", "987654321");
//printing customers
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}");
}

repo.DeleteCustomer(1);
//printing all customers
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}");
}
