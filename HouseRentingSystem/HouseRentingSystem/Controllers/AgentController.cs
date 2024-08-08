using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseRentingSystem.Controllers;

[Authorize]
public class AgentController : Controller
{
	private readonly IAgentService _agentService;

	public AgentController(IAgentService agentService)
	{
		_agentService = agentService;
	}

	public async Task<IActionResult> Become()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Become(BecomeAgentFormModel model)
	{
		var userId = User.Id();

		if (await _agentService.ExistByIdAsync(userId))
		{
			return BadRequest();
		}

		if ( await _agentService.UserWithPhonerNumberExistsAsync(model.PhoneNumber))
		{
			ModelState.AddModelError(nameof(model.PhoneNumber), "Phone number already exists. Enter another one");
		}

		if (await _agentService.UserHasRents(userId))
		{
			ModelState.AddModelError("Error", "You should have no rents to become an agent!");
		}

		if (!ModelState.IsValid)
		{
			return View();
		}

		await _agentService.CreateAsync(userId, model.PhoneNumber);

		return RedirectToAction(nameof(HouseController.All), "House");
	}

}
