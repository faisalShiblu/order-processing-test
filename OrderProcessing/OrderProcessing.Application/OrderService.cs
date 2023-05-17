using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ViewModel;
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

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }
    }
}
