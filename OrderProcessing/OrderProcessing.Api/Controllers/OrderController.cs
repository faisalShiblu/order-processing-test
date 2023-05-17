using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Application;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ViewModel;
using OrderProcessing.Infrustructure;

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
        [Route("GetAllOrders")]
        public IActionResult Get()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet]
        [Route("GetAllOrderById")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }


        [HttpPost]
        [Route("SaveOrder")]
        public IActionResult Post(OrderViewModel order)
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

        [HttpPost]
        [Route("UpdateOrder")]
        public IActionResult Put(int id, OrderViewModel order)
        {

            var old_order = _orderService.GetOrderById(id);
            if (old_order == null)
                return NotFound();


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

            old_order.City = order.City;
            old_order.CustomerName = order.CustomerName;
            old_order.Discount = grossTotalPrice * discount;
            //old_order.OrderDate = System.DateTime.Now;
            old_order.Vat = grossTotalPrice * 0.15m;
            old_order.TotalAmount = grossTotalPrice;
            old_order.NetAmount = grossTotalPrice - (grossTotalPrice * discount) + (grossTotalPrice * 0.15m);

            return Ok(_orderService.UpdateOrder(old_order));
        }

        [HttpPost]
        [Route("DeleteOrder")]
        public IActionResult Delete(int id)
        {
            var count = _orderService.DeleteOrder(id);
            if(count > 0)
                return Ok();
            
            return NotFound();
        }
    }
}
