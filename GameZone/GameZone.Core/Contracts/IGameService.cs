using GameZone.Core.Models;
using GameZone.Data.Models;

namespace GameZone.Core.Contracts;

public interface IGameService
{
	Task<ICollection<AllGameVIewModel>> GetAllGamesAsync();
	
	Task AddgameAsync(AddGameViewModel game, string publisherId);
	
	Task<ICollection<AllGameVIewModel>> GetUserGamesCollectionAsync(string? userId);
	
	Task AddGameToUserCollectionAsync(int gameId, string? userId);
	
	Task RemoveGameFromUserCollectionAsync(int gameId, string? userId);
}
