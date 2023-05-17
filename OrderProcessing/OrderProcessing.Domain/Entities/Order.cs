using System;
using System.Collections.Generic;

namespace OrderProcessing.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public string City { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }  
        public decimal Vat { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
