using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Abstractions
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(Guid id);
        Task AddAsync(Invoice invoice);
        Task<List<Invoice>> GetAllAsync();
    }
}
