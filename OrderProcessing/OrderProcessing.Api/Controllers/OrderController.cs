using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ViewModel;

namespace OrderProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderService.GetAllOrders());
        }


        [HttpPost]
        public IActionResult ProcessOrder(OrderViewModel order)
        {
            // Apply discounts based on the city
            decimal discount = 0;

            switch (order.City.ToUpper())
            {
                case "CHATTOGRAM":
                    discount = 0.15m;
                    break;
                case "DHAKA":
                    discount = 0.20m;
                    break;
                case "KHULNA":
                    discount = 0.10m;
                    break;
            }

            decimal grossTotalPrice = 1000;
            //foreach (var item in order.Items)
            //{
            //    decimal totalPrice = item.Price * item.Quantity;
            //    grossTotalPrice += totalPrice;
            //}

            var newOrder = new Order
            {
                City = order.City,
                CustomerName = order.CustomerName,
                Discount = grossTotalPrice * discount,
                OrderDate = System.DateTime.Now,
                Vat = grossTotalPrice * 0.15m,
                TotalAmount = grossTotalPrice,
                NetAmount = grossTotalPrice - (grossTotalPrice * discount) + (grossTotalPrice * 0.15m),
                OrderId = 0,
               // Items = order.Items
            };

            return Ok(_orderService.AddOrder(newOrder));
        }

    }
}
