using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces.ManagerServices;
using DTO.CategoryDtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace POS.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IServicesManager _servicesManager;

		public CategoryController(IServicesManager servicesManager)
		{
			_servicesManager = servicesManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _servicesManager.CategoryServices.GetAllCategoriesAsync();
			return View(result.Data ?? new List<CategoryResponseDto>());
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var result = await _servicesManager.CategoryServices.GetCategoryByIdAsync(id);
			if (result.StatusCode != 200 || result.Data == null)
				return NotFound(result.Message);
			return View(result.Data);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateCategoryDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var result = await _servicesManager.CategoryServices.CreateCategoryAsync(dto);
			if (result.StatusCode == 200 || result.StatusCode == 201)
				return RedirectToAction(nameof(Index));

			ViewBag.Error = result.Message;
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var result = await _servicesManager.CategoryServices.GetCategoryByIdAsync(id);
			if (result.StatusCode != 200 || result.Data == null)
				return NotFound(result.Message);
			var dto = new UpdateCategoryDto { Id = result.Data.Id, Name = result.Data.Name, Icon = result.Data.Icon };
			return View(dto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateCategoryDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var result = await _servicesManager.CategoryServices.UpdateCategoryAsync(dto.Id, dto);
			if (result.StatusCode == 200)
				return RedirectToAction(nameof(Index));

			ViewBag.Error = result.Message;
			return View(dto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _servicesManager.CategoryServices.DeleteCategoryAsync(id);
			if (result.StatusCode == 200)
				return RedirectToAction(nameof(Index));

			TempData["Error"] = result.Message;
			return RedirectToAction(nameof(Index));
		}
	}
}


