namespace YAES.Application.DTOs
{
    public class InvoiceCreateDto
    {
        public Guid CustomerId { get; set; }
        public List<InvoiceItemCreateDto> Items { get; set; } = [];
    }
}
