using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Api.Models;
using OrderProcessing.Api.ViewModel;

namespace OrderProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

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


            var newOrder = new Order
            {
                City = order.City,
                CustomerName = order.CustomerName,
                Discount = order.TotalAmount * discount,
                OrderDate = System.DateTime.Now,
                Vat = order.TotalAmount * 0.15m,
                TotalAmount = order.TotalAmount,
                NetAmount = order.TotalAmount - (order.TotalAmount * discount) + (order.TotalAmount * 0.15m),
                OrderId = 0
            };
            // Calculate the total price for each ordered item

            // Process the order and return the result
            // You can add your own logic here

            return Ok(newOrder);
        }

    }
}
