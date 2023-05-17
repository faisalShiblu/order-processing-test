using OrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Application
{
    public interface IOrderService
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

        // vat and discount

        decimal GetDiscountService(string cityName, decimal grossAmount);
        decimal GetVatService(decimal grossAmount);

    }
}
