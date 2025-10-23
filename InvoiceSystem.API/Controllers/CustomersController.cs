using InvoiceSystem.Application.DTOs;
using InvoiceSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string email, string phone, string address)
        {
            var customerCreateDto = new CustomerCreateDto
            {
                Name = name,
                Email = email,
                Phone = phone,
                Address = address
            };
            var customerResponseDto = await _customerService.CreateCustomerAsync(customerCreateDto);
            return Ok(customerResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var customerResponseDto = await _customerService.GetByIdAsync(id);
            if (customerResponseDto == null) return NotFound();
            return Ok(customerResponseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customerDtos = await _customerService.GetAllAsync();
            return Ok(customerDtos);
        }
    }
}
