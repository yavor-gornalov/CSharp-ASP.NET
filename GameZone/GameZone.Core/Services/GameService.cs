using GameZone.Core.Contracts;
using GameZone.Core.Models;
using GameZone.Data;
using GameZone.Data.Models;
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
}

