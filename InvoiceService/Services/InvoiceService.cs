using InvoiceService.Data;
using InvoiceService.DTOs;
using InvoiceService.Interfaces;
using InvoiceService.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceDbContext _context;
        public InvoiceService(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            var invoice = new Invoice
            {
                UserId = request.UserId,
                Items = request.Items.Select(item => new InvoiceItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return new InvoiceResponse
            {
                InvoiceId = invoice.Id,
                CreatedAt = invoice.CreatedAt,
                TotalAmount = invoice.TotalAmount
            };
        }

        public async Task<List<InvoiceResponse>> GetAllInvoicesAsync()
        {
            var invoices = await _context.Invoices.Include(i => i.Items)
                .ToListAsync();

            return invoices.Select(invoice => new InvoiceResponse
            {
                InvoiceId = invoice.Id,
                CreatedAt = invoice.CreatedAt,
                TotalAmount = invoice.TotalAmount
            }).ToList();
        }
    }
}
