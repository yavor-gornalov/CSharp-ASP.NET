using HouseRentingSystem.Core.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> All()
        {
            var allUsers = await _userService.AllAsync();

            return View(allUsers);
        }
    }
}
