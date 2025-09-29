using InvoiceSystem.Application.Abstractions;
using InvoiceSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string email, string phone, string address)
        {
            var customer = new Customer(name, email, phone, address);
            await _customerRepo.AddAsync(customer);
            return Ok(customer.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerRepo.GetAllAsync();
            return Ok(customers);
        }
    }
}
