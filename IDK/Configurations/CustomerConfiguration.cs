using IDK.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Configurations
{
    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(CreateCustomers());
        }

        public List<Customer> CreateCustomers()
        {
            List<Customer> customers = new()
            {
                new Customer
                {
                    Id = 1,
                    Name = "Viko",
                    Password = "piko",
                    Address = "123JUMP-STREET",
                    Email = "testinshit@ridah.com"
                },
                new Customer 
                {
                    Id = 2,
                    Name = "Piko",
                    Password = "Viko",
                    Address = "325MainStreet",
                    Email = "AmbitionzAsARidah@2pac.com"
                }
            };
            return customers;
        }
    }
}
