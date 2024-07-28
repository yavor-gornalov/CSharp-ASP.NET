using GameZone.Contracts;
using GameZone.Data;

namespace GameZone.Services;

public class GameService : IGameService
{
	private readonly GameZoneDbContext context;
	public GameService(GameZoneDbContext _context)
	{
		context = _context;
	}
}

