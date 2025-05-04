namespace InvoiceService.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public decimal TotalAmount => Items.Sum(item => item.Price * item.Quantity);
    }
}
