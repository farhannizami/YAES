using YAES.Application.DTOs;
using YAES.Domain.Entities;

namespace YAES.Application.Mappers
{
    public static class InvoiceMapper
    {
        public static InvoiceResponseDto? ToDto (this Invoice invoice)
        {
            if (invoice == null) return null;
            return new InvoiceResponseDto
            {
                Id = invoice.Id,
                Customer = invoice.Customer?.ToDto()!,
                Items = invoice.Items.Select(i => new InvoiceItemDto
                {
                    Id = i.Id,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    LineTotal = i.LineTotal
                }).ToList(),
                TotalAmount = invoice.TotalAmount,
                CreatedAt = invoice.CreatedAt
            };
        }

        public static Invoice? ToEntity(this InvoiceCreateDto dto)
        {
            if (dto == null) return null;

            var invoice = new Invoice(dto.CustomerId);

            if (dto.Items != null)
            {
                foreach (var itemDto in dto.Items)
                {
                    invoice.AddItem(itemDto.Description, itemDto.Quantity, itemDto.UnitPrice);
                }
            }

            return invoice;
        }
    }
}
