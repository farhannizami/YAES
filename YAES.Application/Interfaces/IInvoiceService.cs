using YAES.Application.DTOs;

namespace YAES.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> CreateInvoiceAsync(InvoiceCreateDto dto);
        Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();
        Task<InvoiceResponseDto> GetInvoiceAsync(Guid id);
        Task AddItemsAsync(Guid invoiceId, List<InvoiceItemCreateDto> items);
        Task<bool> DeleteInvoiceAsync(Guid id);
        Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId);
    }
}
