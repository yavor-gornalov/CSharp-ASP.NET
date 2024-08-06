using Microsoft.AspNetCore.Mvc;
using SeminarHub.Models;
using SeminarHub.Services;
using SeminarHub.Services.Contracts;

namespace SeminarHub.Controllers;

public class SeminarController : BaseController
{
	private readonly ISeminarService seminarService;

	public SeminarController(ISeminarService _service)
	{
		seminarService = _service;
	}

	public async Task<IActionResult> All()
	{
		var model = await seminarService.GetAllSeminarsAsync();

		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = new SeminarAddViewModel
		{
			Categories = await seminarService.GetSeminarCategoriesAsync()
		};

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(SeminarAddViewModel model)
	{
		var organizerId = GetUserId();

		if (organizerId == null)
		{
			return Unauthorized();
		}

		if (!ModelState.IsValid)
		{
			model.Categories = await seminarService.GetSeminarCategoriesAsync();
			return View(model);
		}

		await seminarService.AddSeminarAsync(model, organizerId);
		return RedirectToAction(nameof(All));
	}
}
