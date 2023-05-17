using OrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Application
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order AddOrder(Order order)
        {
           return _orderRepository.AddOrder(order);
        }

        public OrderItem AddOrderItems(OrderItem items)
        {
            return _orderRepository.AddOrderItems(items);
        }

        public int DeleteOrder(int id)
        {
            return _orderRepository.DeleteOrder(id);
        }


        public List<OrderItem> GetAllOrderItemsByOrderId(int id)
        {
            return _orderRepository.GetAllOrderItemsByOrderId(id);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public decimal GetDiscountService(string cityName, decimal grossAmount)
        {
            decimal discount = 0;

            switch (cityName.ToUpper())
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
                default:
                    discount = 0;
                    break;
            }

            return grossAmount * discount;
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public decimal GetVatService(decimal grossAmount)
        {
            return grossAmount * 0.15m;
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepository.UpdateOrder(order);
        }

        public OrderItem UpdateOrderItems(OrderItem items)
        {
            return _orderRepository.UpdateOrderItems(items);
        }
    }
}
