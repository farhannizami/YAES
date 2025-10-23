namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceResponseDto
    {
        public Guid Id { get; set; }
        public CustomerDto Customer { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
