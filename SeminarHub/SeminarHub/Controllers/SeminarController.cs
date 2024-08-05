using Microsoft.AspNetCore.Mvc;
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

	public async Task<IActionResult> Add()
	{
		return View();
	}
}
