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
			return Unauthorized();

		if (!ModelState.IsValid)
		{
			model.Categories = await seminarService.GetSeminarCategoriesAsync();
			return View(model);
		}

		await seminarService.AddSeminarAsync(model, organizerId);
		return RedirectToAction(nameof(All));
	}

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var userId = GetUserId();
		if (userId == null)
			return Unauthorized();

		var seminar = await seminarService.GetSeminarForEditAsync(id);

		if (seminar == null)
			return NotFound();

		if (seminar.OrganizerId != userId)
			return BadRequest();

		seminar.Categories = await seminarService.GetSeminarCategoriesAsync();
		return View(seminar);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(int id, SeminarEditViewModel model)
	{
		var userId = GetUserId();
		if (userId == null)
			return Unauthorized();

		if (!ModelState.IsValid)
		{
			model.Categories = await seminarService.GetSeminarCategoriesAsync();
			return View(model);
		}

		try
		{
			await seminarService.EditSeminarAsync(id, userId, model);
		}
		catch (ArgumentException ae)
		{
			ModelState.AddModelError("DateAndTime", ae.Message);
			model.Categories = await seminarService.GetSeminarCategoriesAsync();
			return View(model);
		}
		catch (UnauthorizedAccessException)
		{
			return BadRequest();
		}

		return RedirectToAction(nameof(All));
	}

	public async Task<IActionResult> Join(int id)
	{
		var userId = GetUserId();

		if (userId == null)
		{
			return Unauthorized();
		}

		try
		{
			await seminarService.JoinSeminarAsync(id, userId);

		}
		catch (ArgumentException)
		{
			return RedirectToAction(nameof(All));
		}

		return RedirectToAction(nameof(Joined));



	}

	public async Task<IActionResult> Joined()
	{
		var userId = GetUserId();

		if (userId == null)
		{
			return Unauthorized();
		}

		var seminars = await seminarService.GetUserSeminarsAsync(userId);
		return View(seminars);
	}

	public async Task<IActionResult> Leave(int id)
	{
		var userId = GetUserId();

		if (userId == null)
			return Unauthorized();

		try
		{
			await seminarService.LeaveSeminarAsync(id, userId);

		}
		catch (ArgumentException)
		{
			return BadRequest();
		}

		return RedirectToAction(nameof(Joined));
	}

	public async Task<IActionResult> Details(int id)
	{
		var userId = GetUserId();
		if (userId == null)
			return Unauthorized();

		var seminar = await seminarService.GetSeminarDetailsByIdAsync(id);

		if (seminar == null)
			return NotFound();

		return View(seminar);
	}
}
