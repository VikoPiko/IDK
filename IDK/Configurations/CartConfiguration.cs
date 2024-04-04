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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasData(ConfigureCart());
        }

        public List<Cart> ConfigureCart()
        {
            List<Cart> carts = new List<Cart>()
            {
                new Cart()
                {
                    Id = 1,
                    ProductName = "Air Jordan 4",
                    Price = 299.99M,
                    ProductId = 1,
                    Quantity = 1,
                    userId = 2
                },
                new Cart()
                {
                    Id = 2,
                    ProductName = "Air Jordan 4",
                    Price = 299.99M,
                    ProductId = 1,
                    Quantity = 2,
                    userId = 1
                }
            };
            return carts;
        }
    }
}
