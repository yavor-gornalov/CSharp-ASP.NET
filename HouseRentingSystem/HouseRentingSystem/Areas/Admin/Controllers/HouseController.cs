using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;

namespace HouseRentingSystem.Areas.Admin.Controllers;

public class HouseController : AdminController
{
    private readonly IHouseService _houseService;
    private readonly IAgentService _agentService;

    public HouseController(IHouseService houseService, IAgentService agentService)
    {
        _houseService = houseService;
        _agentService = agentService;
    }

    public async Task<IActionResult> Mine()
    {
        var adminUserId = User.Id();
        var adminAgentId = await _agentService.GetAgentIdAsync(adminUserId);

        var model = new MyHousesViewModel
        {
            AddedHouses = await _houseService.AllHousesByAgentIdAsync(adminAgentId),
            RentedHouses = await _houseService.AllHousesByUserIdAsync(adminUserId)
        };

        return View(model);
    }
}
