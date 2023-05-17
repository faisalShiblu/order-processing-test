using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application;
using OrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderProcessing.Infrustructure
{
    public class OrderRepository : IOrderRepository
    {
        public static List<Order> orders = new List<Order>()
        {
             new Order{City="Dhaka", CustomerName="A", Discount=0, NetAmount=100, OrderDate=DateTime.Now, OrderId=1, TotalAmount=100, Vat=0},
             new Order{City="Dhaka", CustomerName="B", Discount=0, NetAmount=100, OrderDate=DateTime.Now, OrderId=2, TotalAmount=100, Vat=0},
             new Order{City="Dhaka", CustomerName="C", Discount=0, NetAmount=100, OrderDate=DateTime.Now, OrderId=3, TotalAmount=100, Vat=0},
             new Order{City="Dhaka", CustomerName="D", Discount=0, NetAmount=100, OrderDate=DateTime.Now, OrderId=4, TotalAmount=100, Vat=0},
            new Order{City="Dhaka", CustomerName="E", Discount=10, NetAmount=95, OrderDate=DateTime.Now, OrderId=5, TotalAmount=100, Vat=5}
        };
        private readonly OrderDbContext _orderDbContext;

        public OrderRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public Order AddOrder(Order order)
        {
            _orderDbContext.Orders.Add(order);
            _orderDbContext.SaveChanges();
            return order;
        }

        public OrderItem AddOrderItems(OrderItem item)
        {
            _orderDbContext.OrderItems.Add(item);
            _orderDbContext.SaveChanges();
            return item;
        }

        public int DeleteOrder(int id)
        {
            var order = _orderDbContext.Orders.Find(id);
            if (order != null)
            {
                _orderDbContext.Orders.Remove(order);
                _orderDbContext.SaveChanges();

                return 1;
            }
            return 0;
        }


        public List<OrderItem> GetAllOrderItemsByOrderId(int id)
        {
            return _orderDbContext.OrderItems.Where(i => i.Order.OrderId == id).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _orderDbContext.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _orderDbContext.Orders.Find(id);
        }

        public Order UpdateOrder(Order order)
        {
            _orderDbContext.Entry(order).State = EntityState.Modified;
            _orderDbContext.SaveChanges();
            return order;
        }

        public OrderItem UpdateOrderItems(OrderItem item)
        {
            _orderDbContext.Entry(item).State = EntityState.Modified;
            _orderDbContext.SaveChanges();
            return item;
        }
    }
}
