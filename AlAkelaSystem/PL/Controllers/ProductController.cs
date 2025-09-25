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

        // GET: /Product/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
            return View();
        }

        // JSON create endpoint
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _servicesManager.ProductServices.CreateProductAsync(dto);
            return Json(result);
        }

        // Form POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("application/x-www-form-urlencoded", "multipart/form-data")]
        public async Task<IActionResult> Create(CreateProductDto formDto)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
                ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
                return View(formDto);
            }

            var result = await _servicesManager.ProductServices.CreateProductAsync(formDto);
            if (result.StatusCode == 200 || result.StatusCode == 201)
                return RedirectToAction(nameof(Index));

            var cats = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
            ViewBag.Categories = cats.Data ?? new List<CategoryResponseDto>();
            ViewBag.Error = result.Message;
            return View(formDto);
        }

        // GET: /Product/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _servicesManager.ProductServices.GetProductByIdAsync(id);
            if (product.StatusCode != 200 || product.Data == null)
                return NotFound(product.Message);

            var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();

            // Reuse Details model for display; inputs will bind manually
            return View(product.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateProductDto dto)
        {
            var result = await _servicesManager.ProductServices.UpdateProductAsync(id, dto);
            return Json(result);
        }

        // POST: /Product/Edit (form POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductDto dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest();

            var result = await _servicesManager.ProductServices.UpdateProductAsync(dto.Id, dto);
            if (result.StatusCode == 200)
                return RedirectToAction(nameof(Index));

            var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data ?? new List<CategoryResponseDto>();
            ViewBag.Error = result.Message;
            return View(result.Data);
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


