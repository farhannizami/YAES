using InvoiceService.DTOs;
using InvoiceService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest createInvoiceRequest)
        {
            var response = await _invoiceService.CreateInvoiceAsync(createInvoiceRequest);
            return Ok(response);
        }
    }
}
