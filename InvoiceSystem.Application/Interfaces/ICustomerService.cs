using InvoiceSystem.Application.DTOs;

namespace InvoiceSystem.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(Guid id);
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto dto);
    }
}
