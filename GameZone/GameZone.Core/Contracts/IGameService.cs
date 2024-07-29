using GameZone.Core.Models;

namespace GameZone.Core.Contracts;

public interface IGameService
{
	Task<ICollection<AllGameVIewModel>> GetAllGamesAsync();

	Task AddgameAsync(AddGameViewModel game, string publisherId);

	Task<ICollection<AllGameVIewModel>> GetUserGamesCollectionAsync(string? userId);

	Task AddGameToUserCollectionAsync(int gameId, string? userId);

	Task RemoveGameFromUserCollectionAsync(int gameId, string? userId);

	Task<AddGameViewModel> GetGameByIdAsync(int gameId);

	Task EditGameAsync(AddGameViewModel model, int gameId, string publisherId);
}
