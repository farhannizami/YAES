using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly ICustomerRepository _customerRepo;

        public InvoicesController(IInvoiceRepository invoiceRepo, ICustomerRepository customerRepo)
        {
            _invoiceRepo = invoiceRepo;
            _customerRepo = customerRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid customerId)
        {
            var customer = await _customerRepo.GetByIdAsync(customerId);
            if (customer == null) return BadRequest("Customer does not exist");

            var invoice = new Invoice(customerId);
            await _invoiceRepo.AddAsync(invoice);

            return Ok(invoice.Id);
        }

        [HttpPost("{invoiceId}/items")]
        public async Task<IActionResult> AddItem(Guid invoiceId, string description, int quantity, decimal unitPrice)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(invoiceId);
            if (invoice == null) return NotFound("Invoice not found");

            invoice.AddItem(description, quantity, unitPrice);
            await _invoiceRepo.AddAsync(invoice); // Save changes

            return Ok(invoice.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var invoice = await _invoiceRepo.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceRepo.GetAllAsync();
            return Ok(invoices);
        }
    }
}
