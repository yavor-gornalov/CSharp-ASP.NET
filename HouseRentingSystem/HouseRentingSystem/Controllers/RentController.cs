using HouseRentingSystem.Areas.Admin.Controllers;
using HouseRentingSystem.Core.Contracts.Rent;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers
{
    public class RentController : AdminController
    {
        private readonly IRentService _rentService;

        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        public async Task<IActionResult> All()
        {
            var model = await _rentService.AllRentsAsync();

            return View(model);
        }
    }
}
