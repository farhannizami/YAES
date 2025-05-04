namespace InvoiceService.DTOs
{
    public class CreateInvoiceRequest
    {
        public int UserId { get; set; }
        public List<InvoiceItemDTO> Items { get; set; } = new List<InvoiceItemDTO>();
    }
    public class InvoiceItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
