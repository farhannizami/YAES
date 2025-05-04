using InvoiceService.DTOs;

namespace InvoiceService.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponse> CreateInvoiceAsync(CreateInvoiceRequest request);
        Task<List<InvoiceResponse>> GetAllInvoicesAsync();
    }
}
