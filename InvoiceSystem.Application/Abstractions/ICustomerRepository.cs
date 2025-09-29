using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Abstractions
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<List<Customer>> GetAllAsync();
    }
}
