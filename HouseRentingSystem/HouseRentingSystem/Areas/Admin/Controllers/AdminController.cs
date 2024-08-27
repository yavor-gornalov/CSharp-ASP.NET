using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;

namespace HouseRentingSystem.Areas.Admin.Controllers;

[Area(AdminAreaName)]
[Authorize(Roles = AdminRoleName)]
public class AdminController : Controller
{
}
