namespace InvoiceSystem.Domain.Entities
{
    public class InvoiceItem
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal LineTotal => Quantity * UnitPrice;

        public Invoice? Invoice { get; private set; }

        // EF Core needs parameterless constructor
        private InvoiceItem() { }

        // Constructor for creating items
        internal InvoiceItem(Invoice invoice, string description, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            InvoiceId = invoice.Id;
            Invoice = invoice;

            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}