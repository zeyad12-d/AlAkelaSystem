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
        public DbSet<SelectExtrasProduct> SelectExtrasProducts { get; set; } = default!;
        public DbSet<Coupon> Coupons { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configer fluent api
            
            modelBuilder.Entity<Orders>().Property(Orders => Orders.Status).HasConversion<string>();
               
        }
    }
}
