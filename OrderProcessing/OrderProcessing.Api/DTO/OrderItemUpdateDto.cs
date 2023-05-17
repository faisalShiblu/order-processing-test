namespace OrderProcessing.Api.DTO
{
    public class OrderItemUpdateDto
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
