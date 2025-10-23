using InvoiceSystem.Domain.Entities;
using InvoiceSystem.Domain.Interfaces;
using InvoiceSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _db;

        public InvoiceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _db.Invoices.AddAsync(invoice);
            await _db.SaveChangesAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _db.Invoices
                .Include(i => i.Items)
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _db.Invoices
                .Include(i => i.Items)
                .Include(i => i.Customer)
                .ToListAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            // Invoice is already tracked by EF Core, so no need to call Update
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invoice invoice)
        {
            _db.Invoices.Remove(invoice);
            await _db.SaveChangesAsync();
        }
    }
}
