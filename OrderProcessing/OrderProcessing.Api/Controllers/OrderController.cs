using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Api.DTO;
using OrderProcessing.Application;
using OrderProcessing.Domain.Entities;
using OrderProcessing.Infrustructure;
using System.Collections.Generic;
using System.Linq;

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
            var listOfNewOrders = new List<OrderDto>();
            var listOfOrders = _orderService.GetAllOrders();
            if (listOfOrders.Count > 0) {
                foreach (var item in listOfOrders)
                {
                    OrderDto order = new OrderDto();
                    order.OrderDate = item.OrderDate;
                    order.City = item.City;
                    order.CustomerName = item.CustomerName;
                    order.Discount = item.Discount;
                    order.OrderDate = item.OrderDate;
                    order.Vat = item.Vat;
                    order.TotalAmount = item.TotalAmount;
                    order.NetAmount = item.NetAmount;
                    order.Items = _orderService.GetAllOrderItemsByOrderId(item.OrderId)
                        .Select(i => new OrderItemDto
                    {
                        OrderItemId = i.OrderItemId,
                        ProductName = i.ProductName,
                        Price = i.Price,
                        Quantity = i.Quantity
                    }).ToList();
                    order.OrderId = item.OrderId;

                    listOfNewOrders.Add(order);
                }
                return Ok(listOfNewOrders);
            }
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet]
        [Route("GetAllOrderById")]
        public IActionResult GetById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();

            OrderDto orderDto = new OrderDto();
            orderDto.OrderDate = order.OrderDate;
            orderDto.City = order.City;
            orderDto.CustomerName = order.CustomerName;
            orderDto.Discount = order.Discount;
            orderDto.OrderDate = order.OrderDate;
            orderDto.Vat = order.Vat;
            orderDto.TotalAmount = order.TotalAmount;
            orderDto.NetAmount = order.NetAmount;
            orderDto.Items = _orderService.GetAllOrderItemsByOrderId(order.OrderId)
                .Select(i => new OrderItemDto
                {
                    OrderItemId = i.OrderItemId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList();
            orderDto.OrderId = order.OrderId;

            return Ok(orderDto);
        }


        [HttpPost]
        [Route("SaveOrder")]
        public IActionResult Post(OrderCreateDto order)
        {
            decimal grossTotalPrice = 0;
            foreach (var item in order.Items)
            {
                decimal totalPrice = item.Price * item.Quantity;
                grossTotalPrice += totalPrice;
            }

            var newOrder = new Order
            {
                City = order.City,
                CustomerName = order.CustomerName,
                Discount = _orderService.GetDiscountService(order.City, grossTotalPrice),
                OrderDate = System.DateTime.Now,
                Vat = _orderService.GetVatService(grossTotalPrice),
                TotalAmount = grossTotalPrice,
                NetAmount = grossTotalPrice - _orderService.GetDiscountService(order.City, grossTotalPrice) + _orderService.GetVatService(grossTotalPrice),
                OrderId = 0
            };
            var isertedOrder = _orderService.AddOrder(newOrder);

            foreach (var item in order.Items)
            {
                var orderItem = new OrderItem
                {
                    Order = isertedOrder,
                    OrderItemId = 0,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                };

                _orderService.AddOrderItems(orderItem);
            }

            //var insertedOrder = _orderService.GetOrderById(isertedOrder.OrderId);
            //OrderDto orderDto = new OrderDto();
            //orderDto.OrderDate = insertedOrder.OrderDate;
            //orderDto.City = insertedOrder.City;
            //orderDto.CustomerName = insertedOrder.CustomerName;
            //orderDto.Discount = insertedOrder.Discount;
            //orderDto.OrderDate = insertedOrder.OrderDate;
            //orderDto.Vat = insertedOrder.Vat;
            //orderDto.TotalAmount = insertedOrder.TotalAmount;
            //orderDto.NetAmount = insertedOrder.NetAmount;
            //orderDto.Items = _orderService.GetAllOrderItemsByOrderId(insertedOrder.OrderId)
            //    .Select(i => new OrderItemDto
            //    {
            //        OrderItemId = i.OrderItemId,
            //        ProductName = i.ProductName,
            //        Price = i.Price,
            //        Quantity = i.Quantity
            //    }).ToList();
            //orderDto.OrderId = insertedOrder.OrderId;

            //return Ok(orderDto);
             return Ok();
        }

        [HttpPost]
        [Route("UpdateOrder")]
        public IActionResult Put(OrderUpdateDto order)
        {

            var old_order = _orderService.GetOrderById(order.OrderId);
            if (old_order == null)
                return NotFound();

            int itemCount = order.Items.Count;
            int idItemCOunt = order.Items.Where(i => i.OrderItemId > 0).Count();

            if(itemCount != idItemCOunt)
                return BadRequest();

            decimal grossTotalPrice = 0;
            foreach (var item in order.Items)
            {
                decimal totalPrice = item.Price * item.Quantity;
                grossTotalPrice += totalPrice;
            }

            old_order.City = order.City;
            old_order.CustomerName = order.CustomerName;
            old_order.Discount = _orderService.GetDiscountService(order.City, grossTotalPrice);
            old_order.Vat = _orderService.GetVatService(grossTotalPrice);
            old_order.TotalAmount = grossTotalPrice;
            old_order.NetAmount = grossTotalPrice - _orderService.GetDiscountService(order.City, grossTotalPrice) + _orderService.GetVatService(grossTotalPrice);

            var updatedOrder = _orderService.UpdateOrder(old_order);

            foreach (var item in order.Items)
            {
                var orderItem = new OrderItem
                {
                    Order = updatedOrder,
                    OrderItemId = item.OrderItemId,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                };

                _orderService.UpdateOrderItems(orderItem);
            }

            //var insertedOrder = _orderService.GetOrderById(isertedOrder.OrderId);
            //OrderDto orderDto = new OrderDto();
            //orderDto.OrderDate = insertedOrder.OrderDate;
            //orderDto.City = insertedOrder.City;
            //orderDto.CustomerName = insertedOrder.CustomerName;
            //orderDto.Discount = insertedOrder.Discount;
            //orderDto.OrderDate = insertedOrder.OrderDate;
            //orderDto.Vat = insertedOrder.Vat;
            //orderDto.TotalAmount = insertedOrder.TotalAmount;
            //orderDto.NetAmount = insertedOrder.NetAmount;
            //orderDto.Items = _orderService.GetAllOrderItemsByOrderId(insertedOrder.OrderId)
            //    .Select(i => new OrderItemDto
            //    {
            //        OrderItemId = i.OrderItemId,
            //        ProductName = i.ProductName,
            //        Price = i.Price,
            //        Quantity = i.Quantity
            //    }).ToList();
            //orderDto.OrderId = insertedOrder.OrderId;

            //return Ok(orderDto);

            return Ok();
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
