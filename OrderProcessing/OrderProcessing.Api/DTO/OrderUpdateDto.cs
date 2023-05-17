using System.Collections.Generic;

namespace OrderProcessing.Api.DTO
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public string City { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemUpdateDto> Items { get; set; }
    }
}
