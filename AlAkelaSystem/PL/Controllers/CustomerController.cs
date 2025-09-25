using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces.ManagerServices;
using DTO.CustomerDtos;
using System.Threading.Tasks;
using System.Linq;

namespace POS.Controllers
{
	public class CustomerController : Controller
	{
		private readonly IServicesManager _servicesManager;

		public CustomerController(IServicesManager servicesManager)
		{
			_servicesManager = servicesManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return RedirectToAction("Customers", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> GetAll(int pageSize = 50, int page = 1)
		{
			var result = await _servicesManager.CustomerServices.GetAllCustomersAsync(pageSize, page);
			return Json(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _servicesManager.CustomerServices.GetCustomerByIdAsync(id);
			return Json(result);
		}

		[HttpGet]
		public async Task<IActionResult> Search(string term)
		{
			var result = await _servicesManager.CustomerServices.Search(term);
			return Json(result);
		}

		[HttpPost]
		[Consumes("application/json")]
		public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
		{
			var result = await _servicesManager.CustomerServices.CreateCustomerAsync(dto);
			return Json(result);
		}

		[HttpPut]
		[Consumes("application/json")]
		public async Task<IActionResult> Edit(int id, [FromBody] UpdateCustomerDto dto)
		{
			var result = await _servicesManager.CustomerServices.UpdateCustomerAsync(id, dto);
			return Json(result);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _servicesManager.CustomerServices.DeleteCustomerAsync(id);
			return Json(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetTotal(string id)
		{
			var ordersResponse = await _servicesManager.OrderServices.GetOrdersByCustomerIdAsync(id);
			var total = ordersResponse.Data?.Sum(o => o.TotalAmount) ?? 0m;
			return Json(new { StatusCode = 200, Data = new { customerId = id, totalAmount = total } });
		}
	}
}


