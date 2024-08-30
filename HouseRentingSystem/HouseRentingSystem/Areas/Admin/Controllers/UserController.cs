using HouseRentingSystem.Core.Contracts.User;
using HouseRentingSystem.Core.Models.User;
using HouseRentingSystem.Core.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;


namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService _userService;
        private readonly IMemoryCache _memoryCache;

        public UserController(
            IUserService userService,
            IMemoryCache memoryCache)
        {
            _userService = userService;
            _memoryCache = memoryCache;

        }

        public async Task<IActionResult> All()
        {
            var allUsers = _memoryCache.Get<IEnumerable<UserServiceModel>>(UserCacheKey);

            if (allUsers == null)
            {
                allUsers = await _userService.AllAsync();

                _memoryCache.Set(UserCacheKey, allUsers, TimeSpan.FromMinutes(2));
            }

            return View(allUsers);
        }
    }
}
