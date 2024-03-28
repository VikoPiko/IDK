using IDK.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Configurations
{
    class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(CreateAdmin());
        }

        public List<Admin> CreateAdmin() 
        {
            List<Admin> admins = new()
            {
                new Admin
                {
                    Id = 1,
                    Username = "vikopiko",
                    Password = "vikopiko",
                    Email = "vikoasmatiko@gmail.com"
                }
            };
            return admins;
        }
    }
}
