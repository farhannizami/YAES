namespace InvoiceSystem.Application.DTOs
{
    public class InvoiceItemDto
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
