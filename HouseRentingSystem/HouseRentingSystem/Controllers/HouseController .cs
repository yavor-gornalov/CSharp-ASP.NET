using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem.Controllers;

[Authorize]
public class HouseController : Controller
{
	private readonly IHouseService _houseService;
	private readonly IAgentService _agentService;

	public HouseController(IHouseService houseService, IAgentService agentService)
	{
		_houseService = houseService;
		_agentService = agentService;
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
		IEnumerable<HouseServiceModel>? myHouses = null;

		var userId = User.Id();

		if (await _agentService.ExistByIdAsync(userId))
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
	public async Task<IActionResult> Details(int id)
	{
		if (!await _houseService.ExistAsync(id))
		{
			return BadRequest();
		}

		var model = await _houseService.HouseDetailsByIdAsync(id);

		return View(model);
	}

	public async Task<IActionResult> Add()
	{
		var userId = User.Id();
		if (await _agentService.ExistByIdAsync(userId) == false)
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
		if (await _agentService.ExistByIdAsync(User.Id()) == false)
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
		var newHouseId = await _houseService.Create(model, agentId);

		return RedirectToAction(nameof(Details), new { id = newHouseId });

	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		return View(new HouseFormModel());
	}

	[HttpPost]
	public async Task<IActionResult> Edit(int id, HouseFormModel house)
	{
		return RedirectToAction(nameof(Details), new { id = "1" });
	}

	[HttpGet]
	public async Task<IActionResult> Delete(int id)
	{
		return View(new HouseFormModel());
	}

	[HttpPost]
	public async Task<IActionResult> Delete(int id, HouseFormModel house)
	{
		return RedirectToAction(nameof(All));
	}

	[HttpPost]
	public async Task<IActionResult> Rent(int id)
	{
		return RedirectToAction(nameof(Mine));
	}

	[HttpPost]
	public async Task<IActionResult> Leave(int id)
	{
		return RedirectToAction(nameof(Mine));
	}
}
