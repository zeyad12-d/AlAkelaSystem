using DAL.Models;
using System;
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

            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "مشروبات", Icon = "fas fa-coffee" },
                new Category { Id = 2, Name = "أطعمة", Icon = "fas fa-hamburger" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "قهوة تركي", Description = "قهوة تركية ساخنة", Price = 12.50m, Stock = 100, CategoryId = 1 },
                new Product { Id = 2, Name = "شاي", Description = "شاي أخضر", Price = 8.00m, Stock = 150, CategoryId = 1 },
                new Product { Id = 3, Name = "برجر لحم", Description = "برجر مع جبن", Price = 35.00m, Stock = 50, CategoryId = 2 }
            );

            // Extras
            modelBuilder.Entity<Extras>().HasData(
                new Extras { Id = 1, Name = "جبنة إضافية", Price = 3.00m, CategoryId = 2 },
                new Extras { Id = 2, Name = "سكر إضافي", Price = 1.00m, CategoryId = 1 }
            );

            // Customers (fixed GUIDs for deterministic seed)
            var customer1Id = "11111111-1111-1111-1111-111111111111";
            var customer2Id = "22222222-2222-2222-2222-222222222222";
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = customer1Id, Name = "عميل افتراضي", Phone = "0500000001", Address = "الرياض" },
                new Customer { Id = customer2Id, Name = "زائر", Phone = "0500000002", Address = "جدة" }
            );

            // Orders
            modelBuilder.Entity<Orders>().HasData(
                new Orders { Id = 1, OrderDate = new DateTime(2025, 1, 1), PaymentMethod = "Cash", TotalAmount = 21.50m, Status = OrderStatus.Completed, CustomerId = customer1Id },
                new Orders { Id = 2, OrderDate = new DateTime(2025, 1, 2), PaymentMethod = "Card", TotalAmount = 35.00m, Status = OrderStatus.InProgress, CustomerId = customer2Id }
            );

            // OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 12.50m },
                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 9.00m },
                new OrderItem { OrderItemId = 3, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 35.00m }
            );

            modelBuilder.Entity("ExtrasOrderItem").HasData(
                new { ExtrasId = 2, OrderItemsOrderItemId = 1 },
                new { ExtrasId = 1, OrderItemsOrderItemId = 3 }
            );

            // Coupons
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, Code = "WELCOME10", DiscountAmount = 10.00m, ExpiryDate = new DateTime(2026, 12, 31), IsActive = true },
                new Coupon { Id = 2, Code = "SUMMER5", DiscountAmount = 5.00m, ExpiryDate = new DateTime(2026, 6, 30), IsActive = true }
            );

            // Discounts
            modelBuilder.Entity<Discount>().HasData(
                new Discount { Id = 1, DiscountName = "خصم موسم الصيف", DiscountValue = 15 },
                new Discount { Id = 2, DiscountName = "عرض نهاية الأسبوع", DiscountValue = 10 }
            );
        }
    }
}
