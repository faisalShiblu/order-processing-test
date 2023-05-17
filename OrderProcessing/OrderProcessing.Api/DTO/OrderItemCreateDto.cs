namespace OrderProcessing.Api.DTO
{
    public class OrderItemCreateDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
