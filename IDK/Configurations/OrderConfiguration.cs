using IDK.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Configurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(CreateOrders());
        }

        public List<Order> CreateOrders()
        {
            List<Order> orders = new List<Order>()
    {
        new Order
        {
            Id = 1,
            OrderPlaced = DateTime.Parse("2024-03-26"),
            OrderFulfilled = DateTime.Parse("2024-03-27"),
            CustomerId = 1,
            TotalPrice = 299.99M,
            IsComplete = true
        },
        new Order
        {
            Id = 2,
            OrderPlaced = DateTime.Parse("2024-03-28"),
            OrderFulfilled = DateTime.Parse("2024-03-28"),
            CustomerId = 2,
            TotalPrice = 169.98M,
            IsComplete = false
        },
        new Order
        {
            Id = 3,
            OrderPlaced = DateTime.Parse("2024-03-25"),
            OrderFulfilled = DateTime.Parse("2024-03-26"),
            CustomerId = 2,
            TotalPrice = 99.99M,
            IsComplete = true
        },
        new Order
        {
            Id = 4,
            OrderPlaced = DateTime.Parse("2024-03-24"),
            OrderFulfilled = DateTime.Parse("2024-03-25"),
            CustomerId = 1,
            TotalPrice = 449.99M,
            IsComplete = false
        },
        new Order
        {
            Id = 5,
            OrderPlaced = DateTime.Parse("2024-03-23"),
            OrderFulfilled = DateTime.Parse("2024-03-24"),
            CustomerId = 2,
            TotalPrice = 199.99M,
            IsComplete = true
        },
        new Order
        {
            Id = 6,
            OrderPlaced = DateTime.Parse("2024-03-22"),
            OrderFulfilled = DateTime.Parse("2024-03-23"),
            CustomerId = 1,
            TotalPrice = 59.99M,
            IsComplete = false
        },
        new Order
        {
            Id = 7,
            OrderPlaced = DateTime.Parse("2024-03-21"),
            OrderFulfilled = DateTime.Parse("2024-03-22"),
            CustomerId = 1,
            TotalPrice = 389.99M,
            IsComplete = true
        },
        new Order
        {
            Id = 8,
            OrderPlaced = DateTime.Parse("2024-03-20"),
            OrderFulfilled = DateTime.Parse("2024-03-21"),
            CustomerId = 2,
            TotalPrice = 79.99M,
            IsComplete = false
        },
        new Order
        {
            Id = 9,
            OrderPlaced = DateTime.Parse("2024-03-19"),
            OrderFulfilled = DateTime.Parse("2024-03-20"),
            CustomerId = 1,
            TotalPrice = 279.99M,
            IsComplete = true
        },
        new Order
        {
            Id = 10,
            OrderPlaced = DateTime.Parse("2024-03-18"),
            OrderFulfilled = DateTime.Parse("2024-03-19"),
            CustomerId = 1,
            TotalPrice = 149.99M,
            IsComplete = false
        }
    };

            return orders;
        }
    }
}
