using BLL.Interfaces.ManagerServices;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CouponController : Controller
    {
        private readonly IServicesManager _servicesManager;
        public CouponController(IServicesManager servicesManager )
        {
            _servicesManager = servicesManager;

        }
        public async  Task< IActionResult> Index()
        {
            var result =  await _servicesManager.CouponServices.GetAllCouponsAsync();

            return View(result.Data ?? new List<DTO.CouponDtos.CouponResponseDto>());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _servicesManager.CouponServices.GetCouponByIdAsync(id);
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
        public async Task<IActionResult> Create(DTO.CouponDtos.CreateCouponDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var result = await _servicesManager.CouponServices.CreateCouponAsync(dto);
            if (result.StatusCode == 200 || result.StatusCode == 201)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = result.Message;
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _servicesManager.CouponServices.GetCouponByIdAsync(id);
            if (result.StatusCode != 200 || result.Data == null)
                return NotFound(result.Message);
            var dto = new DTO.CouponDtos.UpdateCouponDto
            {
                Code = result.Data.Code,
                DiscountAmount = result.Data.DiscountAmount,
                ExpiryDate = result.Data.ExpiryDate,
                IsActive = result.Data.IsActive
            };
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DTO.CouponDtos.UpdateCouponDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            var result = await _servicesManager.CouponServices.UpdateCouponAsync(id, dto);
            if (result.StatusCode == 200)
                return RedirectToAction(nameof(Index));
            ViewBag.Error = result.Message;
            return View(dto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _servicesManager.CouponServices.DeleteCouponAsync(id);
            if (result.StatusCode == 200)
                return RedirectToAction(nameof(Index));
            return BadRequest(result.Message);
        }
    
    }
}
