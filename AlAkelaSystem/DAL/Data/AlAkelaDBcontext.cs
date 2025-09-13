using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class AlAkelaDBcontext : DbContext
    {
        public AlAkelaDBcontext(DbContextOptions<AlAkelaDBcontext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Extras> Extras { get; set; } = default!;
        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<Orders> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Coupon> Coupons { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configer fluent api

            modelBuilder.Entity<Orders>().Property(Orders => Orders.Status).HasConversion<string>();

            modelBuilder.Entity<OrderItem>()
    .HasMany(o => o.Extras)
    .WithMany(e => e.OrderItems)
    .UsingEntity<Dictionary<string, object>>(
        "ExtrasOrderItem",
        j => j.HasOne<Extras>()
              .WithMany()
              .HasForeignKey("ExtrasId")
              .OnDelete(DeleteBehavior.Cascade),
        j => j.HasOne<OrderItem>()
              .WithMany()
              .HasForeignKey("OrderItemsOrderItemId")
              .OnDelete(DeleteBehavior.Restrict));


            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
