using InvoiceSystem.Application.DTOs;

namespace InvoiceSystem.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> CreateInvoiceAsync(InvoiceCreateDto dto);
        Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();
    }
}
