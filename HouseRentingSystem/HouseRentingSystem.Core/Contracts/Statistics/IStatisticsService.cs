using HouseRentingSystem.Core.Models.Statistics;

namespace HouseRentingSystem.Core.Contracts.Statistics;

public interface IStatisticsService
{
	Task<StatisticServiceModel> TotalAsync();
}
