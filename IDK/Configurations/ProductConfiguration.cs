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
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateProducts());
        }
        public List<Product> CreateProducts() 
        {
            List<Product> products = new ()
            {
                new Product
                {
                    Id = 1,
                    Name = "Air Jordan 4",
                    Price = 299.99M
                },
                new Product 
                {
                    Id= 2,
                    Name = "Air Max 90",
                    Price = 130.00M
                }
            };
            return products;
        }
    }
}
