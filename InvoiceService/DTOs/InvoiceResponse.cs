namespace InvoiceService.DTOs
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
