using iTextSharp.text;
using iTextSharp.text.pdf;
using YAES.Application.DTOs;
using YAES.Application.Interfaces;
using YAES.Application.Mappers;
using YAES.Domain.Interfaces;

namespace YAES.Application.Services
{
    public class InvoiceServices : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceServices(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceResponseDto> CreateInvoiceAsync(InvoiceCreateDto dto)
        {
            var invoice = dto.ToEntity();
            if (invoice == null)
            {
                throw new ArgumentNullException(nameof(dto));                
            }
            await _invoiceRepository.AddAsync(invoice);
            var invoiceDto = invoice.ToDto();
            return invoiceDto!;
        }

        public async Task<List<InvoiceResponseDto>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            if (invoices == null)
            {
                throw new KeyNotFoundException("No invoices found");
            }
            var invoiceDtos = invoices?
                .Select(i => i.ToDto())
                .Where(dto => dto != null)!
                .ToList() ?? [];
            return invoiceDtos!;
        }

        public async Task<InvoiceResponseDto> GetInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }
            var invoiceDto = invoice.ToDto();
            return invoiceDto!;
        }

        public async Task AddItemsAsync(Guid invoiceId, List<InvoiceItemCreateDto> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("No items provided", nameof(items));

            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId)
                ?? throw new KeyNotFoundException("Invoice not found");

            foreach (var item in items)
            {
                invoice.AddItem(item.Description, item.Quantity, item.UnitPrice);
            }

            await _invoiceRepository.UpdateAsync(invoice); // SaveChanges only
        }


        public async Task<bool> DeleteInvoiceAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if(invoice == null)
            {
                return false;
            }

            await _invoiceRepository.DeleteAsync(invoice);
            return true;
        }

        public async Task<byte[]> GenerateInvoicePdfAsync(Guid invoiceId)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new KeyNotFoundException("Invoice not found.");

            var dto = invoice.ToDto();
            if (dto == null)
                throw new InvalidOperationException("Failed to map invoice to DTO.");

            using var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 25, 25, 30, 30);
            var writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Header
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            document.Add(new Paragraph("INVOICE RECEIPT", titleFont));
            document.Add(new Paragraph($"Invoice ID: {dto.Id}", normalFont));
            document.Add(new Paragraph($"Customer: {dto.Customer.Name}", normalFont));
            document.Add(new Paragraph($"Email: {dto.Customer.Email}", normalFont));
            document.Add(new Paragraph($"Date: {dto.CreatedAt}", normalFont));
            document.Add(new Paragraph(" "));

            // Items Table
            var table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.AddCell("Description");
            table.AddCell("Quantity");
            table.AddCell("Unit Price");
            table.AddCell("Line Total");

            foreach (var item in dto.Items)
            {
                table.AddCell(item.Description);
                table.AddCell(item.Quantity.ToString());
                table.AddCell(item.UnitPrice.ToString("C"));
                table.AddCell(item.LineTotal.ToString("C"));
            }

            document.Add(table);
            document.Add(new Paragraph(" "));
            document.Add(new Paragraph($"Total Amount: {dto.TotalAmount:C}", titleFont));

            document.Close();
            writer.Close();

            return memoryStream.ToArray();
        }
    }
}
