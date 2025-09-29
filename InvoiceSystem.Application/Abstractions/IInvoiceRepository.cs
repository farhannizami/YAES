using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Abstractions
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<List<Invoice>> GetAllAsync();
    }
}
