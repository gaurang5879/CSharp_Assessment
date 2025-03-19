using CustomerApp.Models;
using CustomerApp.Repository;

CustomerRepository repo = new();

//Delete all customer
repo.DeleteAllCustomers();

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

//pick first customer
var firstCustomer = customers.First();

//updating a customer
repo.UpdateCustomer(firstCustomer.CustomerID, "test updated", "updatedTest@test.com", "987654321");

//Get customers data
customers = repo.GetCustomers();

//printing customers
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}");
}

//pick first customer for deleting
firstCustomer = customers.First();

//Delete customer with id 1
repo.DeleteCustomer(firstCustomer.CustomerID);

//Get customers data
customers = repo.GetCustomers();

//printing all customers
foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerID}, Name: {customer.Name}, Email: {customer.Email}");
}
