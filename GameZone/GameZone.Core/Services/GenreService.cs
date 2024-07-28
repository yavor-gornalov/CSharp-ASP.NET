using GameZone.Core.Contracts;
using GameZone.Core.Models;
using GameZone.Data;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Core.Services;

public class GenreService : IGenreService
{
	private readonly GameZoneDbContext context;

	public GenreService(GameZoneDbContext _context)
	{
		context = _context;
	}

	public async Task<ICollection<GenreViewModel>> GetAllGenresAsync()
	{
		return await context.Genres
			.Select(g => new GenreViewModel
			{
				Id = g.Id,
				Name = g.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}
}
