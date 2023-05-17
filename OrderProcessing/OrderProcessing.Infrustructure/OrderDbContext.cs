using Microsoft.EntityFrameworkCore;
using OrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessing.Infrustructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Order> Orders { get; set; }
      //  public DbSet<OrderItem> OrderItems { get; set; }
    }
}
