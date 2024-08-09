using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HouseRentingSystem.Controllers
{
    public class HomeController : Controller
	{
		private readonly IHouseService _houseService;

		public HomeController(IHouseService houseService)
		{
			_houseService = houseService;
		}

		public async Task<IActionResult> Index()
		{
			var houses = await _houseService.LastThreeHousesAsync();

			return View(houses);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
