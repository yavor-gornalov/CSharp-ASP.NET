﻿using HouseRentingSystem.Core.Models.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers;

[Authorize]
public class AgentController : Controller
{
	public async Task <IActionResult> Become()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Become (BecomeAgentFormModel agent)
	{
		return RedirectToAction(nameof(HouseController.All), "Houses");
	}

}
