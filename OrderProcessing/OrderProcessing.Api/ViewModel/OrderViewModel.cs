using OrderProcessing.Api.Models;
using System.Collections.Generic;

namespace OrderProcessing.Api.ViewModel
{
    public class OrderViewModel
    {
        public string City { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
