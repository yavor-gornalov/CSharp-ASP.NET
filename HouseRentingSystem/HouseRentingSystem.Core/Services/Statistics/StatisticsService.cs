using HouseRentingSystem.Core.Contracts.Statistics;
using HouseRentingSystem.Core.Models.Statistics;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.Statistics;

public class StatisticsService : IStatisticsService
{
	private readonly ApplicationDbContext _context;

	public StatisticsService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<StatisticServiceModel> TotalAsync()
	{
		var totalHouses = await _context.Houses
			.AsNoTracking()
			.CountAsync();

		var totalRents = await _context.Houses
			.Where(h => h.RenterId != null)
			.AsNoTracking()
			.CountAsync();

		return new StatisticServiceModel
		{
			TotalHouses = totalHouses,
			TotalRents = totalRents
		};
	}
}
