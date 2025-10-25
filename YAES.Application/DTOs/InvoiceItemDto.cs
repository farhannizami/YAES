namespace YAES.Application.DTOs
{
    public class InvoiceItemDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
