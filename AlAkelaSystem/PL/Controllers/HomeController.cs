using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces.ModlesInterface;
using DTO.ProductDtos;
using DTO.CategoryDtos;
using DTO.OrderDtos;
using DTO.CustomerDtos;
using DTO.CouponDtos;
using DTO.DiscountDtos;
using DTO.ExtrasDtos;
using BLL.Interfaces.ManagerServices;

namespace POS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesManager _serviceManager;

        public HomeController(IServicesManager servicesManager )
        {
            _serviceManager = servicesManager;
        }

       
        public async Task<IActionResult> Index()
        {
            var categories = await _serviceManager.CategoryServices.GetAllCategoriesAsync();
            var products = await _serviceManager.ProductServices.GetAllProductsAsync(1, 20);
            var extras = await _serviceManager.ExtrasServices.GetAllExtrasAsync();
            var coupons = await _serviceManager.CouponServices.GetAllCouponsAsync();

            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
            ViewBag.Products = products.Data ?? new List<ProductResponseDto>();
            ViewBag.Extras = extras.Data ?? new List<ExtrasResponseDto>();
            ViewBag.Coupons = coupons.Data ?? new List<CouponResponseDto>();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var result = await _serviceManager.ProductServices.GetProductsByCategoryIdAsync(categoryId);
            return Json(result);
        }

      
        [HttpGet]
        public async Task<IActionResult> SearchProducts(string searchTerm)
        {
            
            var products = await _serviceManager.ProductServices.GetAllProductsAsync(1, 50);
            if (products.Data != null && !string.IsNullOrEmpty(searchTerm))
            {
                var filteredProducts = products.Data.Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                return Json(new { Data = filteredProducts, StatusCode = 200 });
            }
            return Json(products);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var result = await _serviceManager.OrderServices.CreateOrderAsync(orderDto);
            return Json(result);
        }

       
        [HttpGet]
        public async Task<IActionResult> SearchCustomers(string term)
        {
            var result = await _serviceManager.CustomerServices.Search(term);
            return Json(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto customerDto)
        {
            var result = await _serviceManager.CustomerServices.CreateCustomerAsync(customerDto);
            return Json(result);
        }

       
        public async Task<IActionResult> Orders()
        {
            var orders = await _serviceManager.OrderServices.GetAllOrdersAsync();
            return View(orders.Data ?? new List<OrderResponseDto>());
        }

     
        public async Task<IActionResult> Customers()
        {
            var customers = await _serviceManager.CustomerServices.GetAllCustomersAsync(12, 1);
            return View(customers.Data ?? new List<CustomerResponseDto>());
        }

    
        public async Task<IActionResult> Products()
        {
            var products = await _serviceManager.ProductServices.GetAllProductsAsync(1, 50);
            var categories = await _serviceManager.CategoryServices.GetAllCategoriesAsync();

            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
            return View(products.Data ?? new List<ProductResponseDto>());
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _serviceManager.CategoryServices.GetAllCategoriesAsync();
            return View(categories.Data ?? new List<CategoryResponseDto>());
        }

        
        public async Task<IActionResult> Coupons()
        {
            var coupons = await _serviceManager.CouponServices.GetAllCouponsAsync();
            return View(coupons.Data ?? new List<CouponResponseDto>());
        }

       
        public async Task<IActionResult> Discounts()
        {
            var discounts = await _serviceManager.DiscountServices.GetAllDiscountsAsync();
            return View(discounts.Data ?? new List<DiscountResponseDto>());
        }

       
        public async Task<IActionResult> Extras()
        {
            var extras = await _serviceManager.ExtrasServices.GetAllExtrasAsync();
            return View(extras.Data ?? new List<ExtrasResponseDto>());
        }
    }
}
