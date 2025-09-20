using BLL.Interfaces.ManagerServices;
using BLL.Interfaces.ModlesInterface;
using DAL.unitofwork;

namespace BLL.Services
{
    public sealed  class ServicesManager : IServicesManager
    {
        private readonly Lazy<ICategoryServices> _categoryServices;

        private readonly Lazy<IproductServices> _productServices;
        private readonly Lazy<ICustomerServices> _customerServices;

        private readonly Lazy<IOrderServices> _orderServices;
        private readonly Lazy<ICouponServices> _couponServices;
        private readonly Lazy<IDiscountServices> _discountServices;
        private readonly Lazy<IExtrasServices> _extrasServices;

        public ServicesManager( UnitOfWork unitOfWork )
        {
            _categoryServices = new Lazy<ICategoryServices>(() => new CategoryServices(unitOfWork));
        }

    }
}
