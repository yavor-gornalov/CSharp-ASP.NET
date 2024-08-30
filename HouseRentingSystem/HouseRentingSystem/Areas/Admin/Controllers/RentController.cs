using HouseRentingSystem.Core.Contracts.Rent;
using HouseRentingSystem.Core.Models.Rent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HouseRentingSystem.Infrastructure.Common;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class RentController : AdminController
    {
        private readonly IRentService _rentService;
        private readonly IMemoryCache _memoryCache;

        public RentController(
            IRentService rentService,
            IMemoryCache memoryCache)
        {
            _rentService = rentService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> All()
        {
            var allRents = _memoryCache
                .Get<IEnumerable<RentServiceModel>>(AdministratorConstants.RentCacheKey);

            if (allRents == null)
            {
                allRents = await _rentService.AllRentsAsync();

                _memoryCache.Set(AdministratorConstants.RentCacheKey, allRents, TimeSpan.FromMinutes(2));
            }

            return View(allRents);
        }
    }
}
