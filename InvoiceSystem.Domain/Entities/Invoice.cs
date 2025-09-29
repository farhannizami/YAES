namespace InvoiceSystem.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; private set; }

        public Invoice(string customerName, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name cannot be empty");
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");

            Id = Guid.NewGuid();
            CustomerName = customerName;
            CreatedAt = DateTime.UtcNow;
            Amount = amount;
        }

        private Invoice() { } // For EF Core
    }
}
