using OrderProcessing.Domain;
using OrderProcessing.Domain.Entities;
using System.Collections.Generic;

namespace OrderProcessing.Domain.ViewModel
{
    public class OrderViewModel
    {
        public string City { get; set; }
        public string CustomerName { get; set; }
        //public List<OrderItemViewModel> Items { get; set; }
    }
}
