using AutoMapper;
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

        public ServicesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _categoryServices = new Lazy<ICategoryServices>(() => new CategoryService(unitOfWork,  mapper));
            _productServices = new Lazy<IproductServices>(() => new ProductService(unitOfWork, mapper));
            _customerServices = new Lazy<ICustomerServices>(() => new CustomerService(unitOfWork, mapper));
            //_orderServices = new Lazy<IOrderServices>(() => new OrderService(unitOfWork, mapper));
            _couponServices = new Lazy<ICouponServices>(() => new CouponService(unitOfWork, mapper) );
            _discountServices = new Lazy<IDiscountServices>(() => new DiscountService(unitOfWork, mapper));
            _extrasServices = new Lazy<IExtrasServices>(() => new ExtrasService(unitOfWork));
        }

        public ICategoryServices CategoryServices => _categoryServices.Value;
        public IproductServices ProductServices => _productServices.Value;
        public ICustomerServices CustomerServices => _customerServices.Value;
        public IOrderServices OrderServices => _orderServices.Value;
        public ICouponServices CouponServices => _couponServices.Value;
        public IDiscountServices DiscountServices => _discountServices.Value;
        public IExtrasServices ExtrasServices => _extrasServices.Value;
    }
}
