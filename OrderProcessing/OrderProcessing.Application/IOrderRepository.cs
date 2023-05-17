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
    }
}
