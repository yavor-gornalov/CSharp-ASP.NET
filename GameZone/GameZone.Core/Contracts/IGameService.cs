using GameZone.Core.Models;

namespace GameZone.Core.Contracts;

public interface IGameService
{
	Task<ICollection<AllGameVIewModel>> GetAllGamesAsync();

	Task AddgameAsync(AddGameViewModel game, string publisherId);

	Task<ICollection<AllGameVIewModel>> GetUserGamesCollectionAsync(string? userId);

	Task AddGameToUserCollectionAsync(int gameId, string? userId);

	Task RemoveGameFromUserCollectionAsync(int gameId, string? userId);

	Task<EditGameViewModel> GetGameByIdAsync(int gameId);

	Task EditGameAsync(EditGameViewModel model, string publisherId);
}
