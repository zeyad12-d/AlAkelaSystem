using DAL.Models;
using DAL.Repository;
using System.Threading.Tasks;

namespace DAL.unitofwork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> ProductRepo { get; }
        IGenericRepository<Category> CategoryRepo { get; }
        IGenericRepository<Orders> OrderRepo { get; }
        IGenericRepository<OrderItem> OrderItemRepo { get; }
        IGenericRepository<Customer> CustomerRepo { get; }
        IGenericRepository<Discount> DiscountRepo { get; }
        IGenericRepository<Coupon> CouponRepo { get; }
        IGenericRepository<Extras> ExtrasRepo { get; }
        
        Task<int> SaveChangesAsync();
    }
}
