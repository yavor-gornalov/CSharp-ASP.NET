using GameZone.Core.Contracts;
using GameZone.Core.Models;
using GameZone.Core.Services;
using GameZone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers;

public class GameController : BaseController
{
	private readonly IGameService gameService;
	private readonly IGenreService genreService;
	private readonly ILogger<GameController> logger;

	public GameController(
			IGameService _gameService,
			IGenreService _genreService,
			ILogger<GameController> _logger)
	{
		gameService = _gameService;
		genreService = _genreService;
		logger = _logger;
	}

	[AllowAnonymous]
	public async Task<IActionResult> All()
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = new AddGameViewModel
		{
			Genres = await genreService.GetAllGenresAsync()
		};

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddGameViewModel model)
	{

		if (!ModelState.IsValid)
		{
			model.Genres = await genreService.GetAllGenresAsync();
			return View(model);
		}

		try
		{
			var userId = GetCurrentUserId();
			await gameService.AddgameAsync(model, userId);
			return RedirectToAction(nameof(All));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while adding the game.");
			ModelState.AddModelError(string.Empty, "An error occurred while adding the game. Please try again later.");
		}

		model.Genres = await genreService.GetAllGenresAsync();
		return View(model);
	}
}
