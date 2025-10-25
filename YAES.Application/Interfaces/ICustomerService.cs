using YAES.Application.DTOs;

namespace YAES.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(Guid id);
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto dto);
    }
}
