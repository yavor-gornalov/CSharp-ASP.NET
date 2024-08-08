using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.House;

public class HouseService : IHouseService
{
	private readonly ApplicationDbContext _context;

	public HouseService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<ICollection<HouseIndexServiceModel>> LastThreeHousesAsync()
	{
		return await _context.Houses
			.OrderByDescending(h => h.Id)	
			.Select(h => new HouseIndexServiceModel
			{
				Id = h.Id,
				Title = h.Title,
				ImageUrl = h.ImageUrl,
			})
			.Take(3)
			.AsNoTracking()
			.ToListAsync();
	}
}
