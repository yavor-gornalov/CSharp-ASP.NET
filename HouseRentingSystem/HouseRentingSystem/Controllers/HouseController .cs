﻿using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Extensions;
using HouseRentingSystem.Core.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

using static HouseRentingSystem.Infrastructure.Common.AdministratorConstants;
using static HouseRentingSystem.Infrastructure.Common.MessageConstants;

namespace HouseRentingSystem.Controllers;

[Authorize]
public class HouseController : Controller
{
    private readonly IHouseService _houseService;
    private readonly IAgentService _agentService;
    private readonly IMemoryCache _memoryCache;

    public HouseController(
        IHouseService houseService,
        IAgentService agentService,
        IMemoryCache memoryCache)
    {
        _houseService = houseService;
        _agentService = agentService;
        _memoryCache = memoryCache;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
    {
        var queryResult = await _houseService.AllAsync(
            query.Category,
            query.SearchTerm,
            query.Sorting,
            query.CurrentPage,
            AllHousesQueryModel.HousesPerPage);

        query.TotalHousesCount = queryResult.TotalHousesCount;
        query.Houses = queryResult.Houses;

        var houseCategories = await _houseService.AllCategoriesNamesAsync();
        query.Categories = (IEnumerable<string>)houseCategories;

        return View(query);
    }

    public async Task<IActionResult> Mine()
    {
        if (User.IsInRole(AdminRoleName))
        {
            return RedirectToAction("Mine", "House", new { area = AdminAreaName });
        }

        IEnumerable<HouseServiceModel>? myHouses = null;

        var userId = User.Id();

        if (await _agentService.ExistsByIdAsync(userId))
        {
            var currentAgentId = await _agentService.GetAgentIdAsync(userId);
            myHouses = await _houseService.AllHousesByAgentIdAsync(currentAgentId);
        }
        else
        {
            myHouses = await _houseService.AllHousesByUserIdAsync(userId);
        }

        return View(myHouses);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, string information)
    {
        if (!await _houseService.ExistAsync(id))
        {
            return BadRequest();
        }

        var model = await _houseService.HouseDetailsByIdAsync(id);

        if (information != model.GetInformation())
        {
            return BadRequest();
        }

        return View(model);
    }

    public async Task<IActionResult> Add()
    {
        var userId = User.Id();
        if (await _agentService.ExistsByIdAsync(userId) == false)
        {
            return RedirectToAction(nameof(AgentController.Become), "Agent");
        }

        var model = new HouseFormModel
        {
            Categories = await _houseService.AllCategoriesAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(HouseFormModel model)
    {
        if (await _agentService.ExistsByIdAsync(User.Id()) == false)
        {
            return RedirectToAction(nameof(AgentController.Become), "Agent");
        }

        if (await _houseService.CategoryExistsAsync(model.CategoryId) == false)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
        }

        if (!ModelState.IsValid)
        {
            model.Categories = await _houseService.AllCategoriesAsync();
            return View(model);

        }

        var userId = User.Id();
        var agentId = await _agentService.GetAgentIdAsync(userId);
        var newHouseId = await _houseService.CreateAsync(model, agentId);

        TempData["success"] = string.Format(AddedHouse, model.Title);

        return RedirectToAction(nameof(Details), new { id = newHouseId, information = model.GetInformation() });

    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (await _houseService.ExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await _houseService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
        {
            return Unauthorized();
        }

        var house = await _houseService.HouseDetailsByIdAsync(id);

        var houseCategoryId = await _houseService.GetHouseCategoryAsync(house.Id);

        var houseModel = new HouseFormModel
        {
            Title = house.Title,
            Address = house.Address,
            Description = house.Description,
            ImageUrl = house.ImageUrl,
            PricePerMonth = house.PricePerMonth,
            CategoryId = houseCategoryId,
            Categories = await _houseService.AllCategoriesAsync(),
        };

        return View(houseModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, HouseFormModel house)
    {
        if (await _houseService.ExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await _houseService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
        {
            return Unauthorized();
        }

        if (await _houseService.CategoryExistsAsync(house.CategoryId) == false)
        {
            ModelState.AddModelError(nameof(house.CategoryId), "Category does not exists");
        }

        if (!ModelState.IsValid)
        {
            house.Categories = await _houseService.AllCategoriesAsync();
            return View(house);
        }

        await _houseService.EditAsync(id, house);

        TempData["warning"] = string.Format(EditedHouse, house.Title);

        return RedirectToAction(nameof(Details), new { id, information = house.GetInformation() });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (await _houseService.ExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await _houseService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
        {
            return Unauthorized();
        }

        var house = await _houseService.HouseDetailsByIdAsync(id);

        var model = new HouseDetailsViewModel
        {
            Id = house.Id,
            Title = house.Title,
            Address = house.Address,
            ImageUrl = house.ImageUrl,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, HouseDetailsViewModel house)
    {
        if (await _houseService.ExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await _houseService.HasAgentWithIdAsync(id, User.Id()) == false && User.IsAdmin() == false)
        {
            return Unauthorized();
        }

        await _houseService.DeleteAsync(house.Id);

        TempData["warning"] = string.Format(DeletedHouse, house.Title);

        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    public async Task<IActionResult> Rent(int id)
    {
        if (await _houseService.ExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await _agentService.ExistsByIdAsync(User.Id()) && User.IsAdmin() == false)
        {
            return Unauthorized();
        }

        var house = await _houseService.HouseDetailsByIdAsync(id);

        if (house.IsRented)
        {
            return BadRequest();
        }

        await _houseService.RentAsync(house.Id, User.Id());
        _memoryCache.Remove(RentCacheKey);

        TempData["success"] = string.Format(RentedHouse, house.Title, house.PricePerMonth);

        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    public async Task<IActionResult> Leave(int id)
    {
        var house = await _houseService.HouseDetailsByIdAsync(id);

        if (house == null || house.IsRented == false)
        {
            return BadRequest();
        }

        if (await _houseService.IsRentedByUserWithIdAsync(id, User.Id()) == false)
        {
            if (User.IsAdmin() == false)
            {
                return Unauthorized();
            }
        }

        await _houseService.LeaveAsync(id);
        _memoryCache.Remove(RentCacheKey);

        TempData["warning"] = string.Format(LeftHouse, house.Title);

        return RedirectToAction(nameof(Mine));
    }
}
