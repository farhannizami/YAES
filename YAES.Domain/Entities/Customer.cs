namespace YAES.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; } 
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Address { get; private set; }

        public List<Invoice> Invoices { get; private set; } = new();

        // EF Core requires parameterless constructor
        private Customer() { }

        // Constructor for creating a new customer
        public Customer(string name, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            // Optionally, you can add email format validation

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Update customer details
        public void UpdateDetails(string name, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required");

            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }

        // Optional: Add invoice for this customer
        internal void AddInvoice(Invoice invoice)
        {
            ArgumentNullException.ThrowIfNull(invoice);

            Invoices.Add(invoice);
        }
    }
}
