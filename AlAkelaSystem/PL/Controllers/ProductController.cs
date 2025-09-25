using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces.ManagerServices;
using DTO.ProductDtos;
using DTO.CategoryDtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace POS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IServicesManager _servicesManager;

        public ProductController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

      
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 50)
        {
            var products = await _servicesManager.ProductServices.GetAllProductsAsync(page, pageSize);
            var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();

            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
            ViewBag.Products = products.Data ?? new List<ProductResponseDto>();
            return View(products.Data ?? new List<ProductResponseDto>());
        }

    
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _servicesManager.ProductServices.GetProductByIdAsync(id);
            if (result.StatusCode != 200 || result.Data == null)
                return NotFound(result.Message);

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _servicesManager.ProductServices.CreateProductAsync(dto);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateProductDto dto)
        {
            var result = await _servicesManager.ProductServices.UpdateProductAsync(id, dto);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _servicesManager.ProductServices.DeleteProductAsync(id);
            return Json(result);
        }

     
        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 50)
        {
            var result = await _servicesManager.ProductServices.GetAllProductsAsync(page, pageSize);
            return Json(result);
        }

     
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _servicesManager.ProductServices.GetProductByIdAsync(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var result = await _servicesManager.ProductServices.GetProductsByCategoryIdAsync(categoryId);
            return Json(result);
        }

     
        [HttpGet]
        public async Task<IActionResult> Search(string term, int page = 1, int pageSize = 50)
        {
            var products = await _servicesManager.ProductServices.GetAllProductsAsync(page, pageSize);
            if (products.Data != null && !string.IsNullOrWhiteSpace(term))
            {
                var filtered = products.Data.Where(p => p.Name.Contains(term, System.StringComparison.OrdinalIgnoreCase));
                return Json(new { Data = filtered, StatusCode = 200 });
            }
            return Json(products);
        }
    }
}


