using HouseRentingSystem.Core.Contracts.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace HouseRentingSystem.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class StatisticController : ControllerBase
	{
		private readonly IStatisticsService _statisticsService;

		public StatisticController(IStatisticsService statisticsService)
		{
			_statisticsService = statisticsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetStatistics()
		{
			var result = await _statisticsService.TotalAsync();

			return Ok(result);
		}
	}
}
