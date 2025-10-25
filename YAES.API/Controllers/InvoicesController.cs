using Microsoft.AspNetCore.Mvc;
using YAES.Application.DTOs;
using YAES.Application.Interfaces;

namespace YAES.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;

        public InvoicesController(IInvoiceService invoiceService, ICustomerService customerService)
        {
            _invoiceService = invoiceService;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid invoice data");
            }

            var customerResponseDto = await _customerService.GetByIdAsync(dto.CustomerId);

            if (customerResponseDto == null)
            {
                return BadRequest("Customer does not exist");
            }

            var invoice = await _invoiceService.CreateInvoiceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
        }

        [HttpPost("{invoiceId:guid}/items")]
        public async Task<IActionResult> AddItems(Guid invoiceId, [FromBody] List<InvoiceItemCreateDto> items)
        {
            if (items == null || items.Count == 0)
            {
                return BadRequest("No items provided");
            }

            try
            {
                await _invoiceService.AddItemsAsync(invoiceId, items);
                return Ok("Items added successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Invoice not found");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceAsync(id);
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            if (invoices == null || invoices.Count == 0)
            {
                return Ok(new List<InvoiceResponseDto>());
            }
            return Ok(invoices);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var invoice = await _invoiceService.GetInvoiceAsync(id);
            if (invoice == null)
            {
                return NotFound("Invoice not found.");
            }

            var deleted = await _invoiceService.DeleteInvoiceAsync(id);
            if (!deleted)
            {
                return NotFound("Invoice not found.");
            }
            return NoContent();
        }

        [HttpGet("{id:guid}/receipt")]
        public async Task<IActionResult> DownloadReceipt(Guid id)
        {
            var fileBytes = await _invoiceService.GenerateInvoicePdfAsync(id);
            if (fileBytes == null)
                return NotFound("Invoice not found or PDF generation failed.");

            return File(fileBytes, "application/pdf", $"Invoice_{id}.pdf");
        }
    }
}
