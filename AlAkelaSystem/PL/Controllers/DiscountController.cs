using BLL.Interfaces.ManagerServices;
using DTO.DiscountDtos;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IServicesManager _servicesManager;
        public DiscountController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }
        public async Task< IActionResult >Index()
        {
            var result = await _servicesManager.DiscountServices.GetAllDiscountsAsync();

            return View(result.Data ?? new List<DiscountResponseDto>());
        }
        [HttpGet]
        public async Task< IActionResult> Details( int id )
        {
            var result =  await _servicesManager.DiscountServices.GetDiscountByIdAsync(id);
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
        public async Task<IActionResult> Create(CreateDiscountDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var result = await _servicesManager.DiscountServices.CreateDiscountAsync(dto);
            if (result.StatusCode == 200 || result.StatusCode == 201)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = result.Message;
            return View(dto);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _servicesManager.DiscountServices.GetDiscountByIdAsync(id);
            if (result.StatusCode != 200 || result.Data == null)
                return NotFound(result.Message);
            var dto = new UpdateDiscountDto
            {
                discountName = result.Data.discountName,
                discountValue = result.Data.discountValue
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateDiscountDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var result = await _servicesManager.DiscountServices.UpdateDiscountAsync(id, dto);
            if (result.StatusCode == 200)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = result.Message;
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _servicesManager.DiscountServices.GetDiscountByIdAsync(id);
            if (result.StatusCode != 200 || result.Data == null)
                return NotFound(result.Message);
            return View(result.Data);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _servicesManager.DiscountServices.DeleteDiscountAsync(id);
            if (result.StatusCode == 200)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = result.Message;
            var discount = await _servicesManager.DiscountServices.GetDiscountByIdAsync(id);
            return View(discount.Data);
        }
    }
}
