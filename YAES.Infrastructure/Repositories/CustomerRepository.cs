using Microsoft.EntityFrameworkCore;
using YAES.Domain.Entities;
using YAES.Domain.Interfaces;
using YAES.Infrastructure.Data;

namespace YAES.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _db.Customers
                .Include(c => c.Invoices) // Include invoices for this customer
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _db.Customers
                .Include(c => c.Invoices) // Optional: include invoices
                .ToListAsync();
        }
    }
}
