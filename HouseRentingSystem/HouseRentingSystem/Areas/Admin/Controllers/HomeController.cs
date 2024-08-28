using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;

namespace HouseRentingSystem.Areas.Admin.Controllers;

public class HomeController : AdminController
{
    public IActionResult Index()
    {
        if (User.IsInRole(AdminRoleName))
        {
            return View();
        }
        return Unauthorized();
    }
}
