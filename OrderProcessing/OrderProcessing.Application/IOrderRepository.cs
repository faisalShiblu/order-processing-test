using OrderProcessing.Domain.Entities;
using OrderProcessing.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Application
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order AddOrder(Order order);
        Order GetOrderById(int id);
        Order UpdateOrder(Order order);
        int DeleteOrder(int id);
    }
}
