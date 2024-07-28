using GameZone.Core.Models;
using GameZone.Data.Models;

namespace GameZone.Core.Contracts;

public interface IGameService
{
	Task AddgameAsync(AddGameViewModel game, string publisherId);
}
