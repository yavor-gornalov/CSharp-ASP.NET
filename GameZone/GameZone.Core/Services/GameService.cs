using GameZone.Core.Contracts;
using GameZone.Core.Models;
using GameZone.Data;
using GameZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static GameZone.Infrastructure.Validations.GlobalConstants;

namespace GameZone.Services;

public class GameService : IGameService
{
	private readonly GameZoneDbContext context;
	public GameService(GameZoneDbContext _context)
	{
		context = _context;
	}

	public async Task AddgameAsync(AddGameViewModel game, string publisherId)
	{
		var isReleaseOnDateValid = DateTime.TryParseExact(
			game.ReleasedOn,
			DateTimeDefaultFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out DateTime releaseOn);

		if (!isReleaseOnDateValid)
			throw new ArgumentException("Invalid date");


		var newGame = new Game
		{
			Name = game.Title,
			Description = game.Description,
			GenreId = game.GenreId,
			ImageUrl = game.ImageUrl,
			ReleasedOn = releaseOn,
			PublisherId = publisherId,
		};

		await context.Games.AddAsync(newGame);
		await context.SaveChangesAsync();
	}

	public async Task AddGameToUserCollectionAsync(int gameId, string? userId)
	{
		if (userId == null) throw new ArgumentNullException("Invalid user");

		var currentGame = context.Games.First(x => x.Id == gameId) ?? throw new ArgumentException("Invalid game.");

		currentGame.GamersGames.Add(new GamerGame
		{
			Game = currentGame,
			GamerId = userId
		});

		await context.SaveChangesAsync();
	}

	public async Task EditGameAsync(AddGameViewModel model, int gameId, string publisherId)
	{
		var game = context.Games.First(g => g.Id == gameId) ?? throw new ArgumentException("Invalid game.");

		if (game.PublisherId != publisherId)
			throw new UnauthorizedAccessException("No privileges to modify this game.");

		var isReleaseOnDateValid = DateTime.TryParseExact(
			model.ReleasedOn,
			DateTimeDefaultFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out DateTime releaseOn);

		if (!isReleaseOnDateValid)
			throw new ArgumentException("Invalid date");

		game.Name = model.Title;
		game.ImageUrl = model.ImageUrl;
		game.Description = model.Description;
		game.ReleasedOn = releaseOn;

		await context.SaveChangesAsync();
	}

	public async Task<ICollection<AllGameVIewModel>> GetAllGamesAsync()
	{
		return await context.Games
			.Select(g => new AllGameVIewModel
			{
				Id = g.Id,
				Title = g.Name,
				ImageUrl = g.ImageUrl,
				Description = g.Description,
				ReleasedOn = g.ReleasedOn.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
				Publisher = g.Publisher.UserName,
				Genre = g.Genre.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<AddGameViewModel> GetGameByIdAsync(int gameId)
	{
		return await context.Games
			.Where(g => g.Id == gameId)
			.Select(g => new AddGameViewModel
			{
				Title = g.Name,
				ImageUrl = g.ImageUrl,
				Description = g.Description,
				GenreId = g.GenreId,
				ReleasedOn = g.ReleasedOn.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
			})
			.AsNoTracking()
			.FirstAsync() ?? throw new ArgumentException("Invalid game.");
	}

	public async Task<ICollection<AllGameVIewModel>> GetUserGamesCollectionAsync(string? userId)
	{
		return await context.Games
			.Where(g => g.GamersGames.Any(gg => gg.Gamer.Id == userId))
			.Select(g => new AllGameVIewModel
			{
				Id = g.Id,
				Title = g.Name,
				ImageUrl = g.ImageUrl,
				Description = g.Description,
				Genre = g.Genre.Name,
				Publisher = g.Publisher.UserName,
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task RemoveGameFromUserCollectionAsync(int gameId, string? userId)
	{
		if (userId == null) throw new ArgumentNullException("Invalid user");

		var currentGame = context.Games
			.Include(g => g.GamersGames)
			.First(x => x.Id == gameId) ?? throw new ArgumentException("Invalid game.");

		var userGameToRemove = currentGame.GamersGames.First(g => g.GamerId == userId);

		currentGame.GamersGames.Remove(userGameToRemove);

		await context.SaveChangesAsync();
	}
}

