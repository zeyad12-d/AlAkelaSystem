using BLL.Interfaces.ManagerServices;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IServicesManager _servicesManager;

        public CategoryController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var categories = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await _servicesManager.CategoryServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DTO.CategoryDtos.CreateCategoryDto createCategoryDto)
        {
            if (ModelState.IsValid)
            {
                await _servicesManager.CategoryServices.CreateCategoryAsync(createCategoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(createCategoryDto);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _servicesManager.CategoryServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DTO.CategoryDtos.UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _servicesManager.CategoryServices.UpdateCategoryAsync(id, updateCategoryDto);
                return RedirectToAction(nameof(Index));
            }
            return View(updateCategoryDto);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _servicesManager.CategoryServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _servicesManager.CategoryServices.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
