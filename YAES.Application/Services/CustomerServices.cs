using YAES.Application.DTOs;
using YAES.Application.Interfaces;
using YAES.Application.Mappers;
using YAES.Domain.Interfaces;

namespace YAES.Application.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> GetByIdAsync(Guid Id)
        {
            var customer = await _customerRepository.GetByIdAsync(Id);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }
            var customerDto = customer.ToDto();
            return customerDto!;
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            var customerDtos = customers
                .Select(c => c.ToDto())
                .Where (dto => dto != null)!
                .ToList();
            return customerDtos!;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerCreateDto dto)
        {
            var customer = dto.ToEntity();

            if (customer == null) 
            {
                throw new ArgumentException("Invalid customer data");
            }

            await _customerRepository.AddAsync(customer);
            var customerDto = customer.ToDto();
            return customerDto!;

        }
    }
}
