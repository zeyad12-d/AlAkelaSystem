using BLL.Interfaces.ModlesInterface;

namespace BLL.Interfaces.ManagerServices
{
    public interface IServicesManager
    {
        ICategoryServices CategoryServices { get; }
        IproductServices ProductServices { get; }
        ICustomerServices CustomerServices { get; }
        IOrderServices OrderServices { get; }
        ICouponServices CouponServices { get; }
        IDiscountServices DiscountServices { get; }
        IExtrasServices ExtrasServices { get; }
    }
}
