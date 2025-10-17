using InvoiceSystem.Application.DTOs;
using InvoiceSystem.Domain.Entities;

namespace InvoiceSystem.Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto? ToDto (this Customer customer)
        {
            if (customer == null) return null;
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public static Customer? ToEntity(this CustomerCreateDto customerCreateDto)
        {
            if (customerCreateDto == null) return null;
            return new Customer(
                customerCreateDto.Name,
                customerCreateDto.Email,
                customerCreateDto.Phone,
                customerCreateDto.Address
            );

        }
    }
}
