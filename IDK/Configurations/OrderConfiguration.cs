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
            List<Order> orders = new()
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
                }
            };
            return orders;
        }
    }
}
