using OrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Application
{
    public interface IOrderRepository
    {
        // parent 
        List<Order> GetAllOrders();
        Order AddOrder(Order order);
        Order GetOrderById(int id);
        Order UpdateOrder(Order order);
        int DeleteOrder(int id);

        // child 
        OrderItem AddOrderItems(OrderItem order);
        OrderItem UpdateOrderItems(OrderItem order);
        List<OrderItem> GetAllOrderItemsByOrderId(int id);
    }
}
