namespace InvoiceSystem.Domain.Entities
    {
        public class Invoice
        {
            public Guid Id { get; private set; }              // Primary Key
            public Guid CustomerId { get; private set; }      // Foreign Key
            public DateTime CreatedAt { get; private set; }
            public DateTime DueDate { get; private set; }
            public decimal TotalAmount { get; private set; }
            public Customer? Customer { get; private set; }   // Navigation property
            public List<InvoiceItem> Items { get; private set; } = [];

            // EF Core requires a parameterless constructor
            private Invoice() { }

            // Constructor to create a valid invoice
            public Invoice(Guid customerId, DateTime dueDate)
            {
                if (dueDate < DateTime.UtcNow)
                    throw new ArgumentException("Due date must be in the future");

                Id = Guid.NewGuid();
                CustomerId = customerId;
                CreatedAt = DateTime.UtcNow;
                DueDate = dueDate;
                TotalAmount = 0m;
            }

            // Add an item to invoice
            public void AddItem(string description, int quantity, decimal unitPrice)
            {
                if (string.IsNullOrWhiteSpace(description))
                    throw new ArgumentException("Description is required");

                if (quantity <= 0)
                    throw new ArgumentException("Quantity must be positive");

                if (unitPrice <= 0)
                    throw new ArgumentException("Unit price must be positive");

                var item = new InvoiceItem(this, description, quantity, unitPrice);
                Items.Add(item);

                RecalculateTotal();
            }

            private void RecalculateTotal()
            {
                decimal total = 0;
                foreach (var item in Items)
                    total += item.LineTotal;

                TotalAmount = total;
            }
        }
}
