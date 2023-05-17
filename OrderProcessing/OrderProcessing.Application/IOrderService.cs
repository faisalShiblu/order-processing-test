using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Application
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order AddOrder(Order order);
        Order UpdateOrder(Order order);
        int DeleteOrder(int id);
    }
}
