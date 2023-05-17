using System.Collections.Generic;

namespace OrderProcessing.Api.DTO
{
    public class OrderCreateDto
    {
        public string City { get; set; }
        public string CustomerName { get; set; }
        public List<OrderItemCreateDto> Items { get; set; }
    }
}
