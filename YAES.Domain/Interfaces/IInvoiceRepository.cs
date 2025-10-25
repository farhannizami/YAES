using YAES.Domain.Entities;

namespace YAES.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<List<Invoice>> GetAllAsync();
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Invoice invoice);
    }
}
