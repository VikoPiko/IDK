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
    class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasData(CreateOrderProducts());
        }

        public List<OrderProduct> CreateOrderProducts()
        {
            List<OrderProduct> orderProducts = new()
            {
                new OrderProduct
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Price = 130.00M
                }
            };
            return orderProducts;
        }
    }
}
