namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceItemCreateDto
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
