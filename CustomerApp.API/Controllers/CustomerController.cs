using CustomerApp.Data.Models;
using CustomerApp.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCustomers() => Ok(_repository.GetCustomers());

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _repository.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (_repository.UpdateCustomer(id, customer.Name, customer.Email, customer.PhoneNumber))
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (_repository.DeleteCustomer(id))
                return NoContent();
            return NotFound();
        }
    }
}
