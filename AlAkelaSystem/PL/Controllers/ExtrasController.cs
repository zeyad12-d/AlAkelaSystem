using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces.ManagerServices;
using DTO.ExtrasDtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace POS.Controllers
{
	public class ExtrasController : Controller
	{
		private readonly IServicesManager _servicesManager;

		public ExtrasController(IServicesManager servicesManager)
		{
			_servicesManager = servicesManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var result = await _servicesManager.ExtrasServices.GetAllExtrasAsync();
			return View(result.Data ?? new List<ExtrasResponseDto>());
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var result = await _servicesManager.ExtrasServices.GetExtrasByIdAsync(id);
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
		public async Task<IActionResult> Create(CreateExtrasDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var result = await _servicesManager.ExtrasServices.CreateExtrasAsync(dto);
			if (result.StatusCode == 200 || result.StatusCode == 201)
				return RedirectToAction(nameof(Index));

			ViewBag.Error = result.Message;
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var result = await _servicesManager.ExtrasServices.GetExtrasByIdAsync(id);
			if (result.StatusCode != 200 || result.Data == null)
				return NotFound(result.Message);
			var dto = new UpdateExtrasDto {id= result.Data.Id, Name = result.Data.Name, Price = result.Data.Price };
			return View(dto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateExtrasDto dto)
		{
			if (!ModelState.IsValid)
				return View(dto);

			var result = await _servicesManager.ExtrasServices.UpdateExtrasAsync(dto.id, dto);
			if (result.StatusCode == 200)
				return RedirectToAction(nameof(Index));

			ViewBag.Error = result.Message;
			return View(dto);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _servicesManager.ExtrasServices.DeleteExtrasAsync(id);
			if (result.StatusCode == 200)
				return RedirectToAction(nameof(Index));

			TempData["Error"] = result.Message;
			return RedirectToAction(nameof(Index));
		}
	}
}


