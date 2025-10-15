namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceCreateDto
    {
        public Guid CustomerId { get; set; }
        public List<InvoiceItemCreateDto> Items { get; set; } = [];
    }
}
