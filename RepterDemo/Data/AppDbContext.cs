using Microsoft.EntityFrameworkCore;
using RepterDemo.Models;

namespace RepterDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<Cart> Cart { get; set; }  

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<ShippingDetails> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItems>()
                .HasKey(oi => new { oi.OrderID, oi.ProductID });
        }
    }

}
