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
		var model = await gameService.GetAllGamesAsync();
		return View(model);
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
			await gameService.AddgameAsync(model, userId!);
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

	[HttpGet]
	public async Task<IActionResult> Edit(int id)
	{
		var userId = GetCurrentUserId()!;
		var model = await gameService.GetGameByIdAsync(id);
		model.Genres = await genreService.GetAllGenresAsync();

		if (model.PublisherId != userId)
		{
			return RedirectToAction(nameof(All));
		}

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Edit(int id, EditGameViewModel model)
	{
		var userId = GetCurrentUserId();
		try
		{
			model.PublisherId = userId!;
			model.Genres = await genreService.GetAllGenresAsync();

			if (ModelState.IsValid)
			{
				await gameService.EditGameAsync(model, id, userId!);
				return RedirectToAction(nameof(All));
			}
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while modifying the game.");
			ModelState.AddModelError(string.Empty, "An error occurred while modifying the game. Please try again later.");
		}

		model.PublisherId = userId!;
		model.Genres = await genreService.GetAllGenresAsync();
		return View(model);
	}

	[HttpGet]
	public async Task<IActionResult> MyZone()
	{
		try
		{
			var userId = GetCurrentUserId();
			var model = await gameService.GetUserGamesCollectionAsync(userId);
			return View(model);

		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while loading user game collection.");
			ModelState.AddModelError(string.Empty, "An error occurred while loading user game collection. Please try again later.");
			return RedirectToAction(nameof(All));
		}
	}

	public async Task<IActionResult> AddToMyZone(int id)
	{
		try
		{
			var userId = GetCurrentUserId();
			await gameService.AddGameToUserCollectionAsync(id, userId);
			return RedirectToAction(nameof(MyZone));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while adding game to user collection.");
			ModelState.AddModelError(string.Empty, "An error occurred while adding game to user collection. Please try again later.");
			return RedirectToAction(nameof(All));
		}
	}

	public async Task<IActionResult> StrikeOut(int id)
	{
		try
		{
			var userId = GetCurrentUserId();
			await gameService.RemoveGameFromUserCollectionAsync(id, userId);
			return RedirectToAction(nameof(MyZone));
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "An error occurred while removin game from user collection.");
			ModelState.AddModelError(string.Empty, "An error occurred while removin game from user collection. Please try again later.");
			return RedirectToAction(nameof(All));
		}
	}

}
