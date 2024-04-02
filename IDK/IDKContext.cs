using Microsoft.EntityFrameworkCore;
using IDK.Infrastructure.Models;
using IDK.Configurations;

namespace IDK
{
    class IDKContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        //DESKTOP-HC94VC5\SQLEXPRESS01 desktop string;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Laptop
            /*optionsBuilder.UseSqlServer("Data Source=DESKTOP-87GDKF5\\SQLEXPRESS; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");*/
            //Desktop
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HC94VC5\\SQLEXPRESS01; Initial Catalog = IDK;" +
                " Integrated Security = True;TrustServerCertificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
