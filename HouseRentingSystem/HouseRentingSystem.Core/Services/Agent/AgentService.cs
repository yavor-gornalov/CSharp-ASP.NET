using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.Agent;

public class AgentService : IAgentService
{
	private readonly ApplicationDbContext _context;

	public AgentService(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task CreateAsync(string userId, string phoneNumber)
	{
		var agent = new Infrastructure.Data.Models.Agent()
		{
			UserId = userId,
			PhoneNumber = phoneNumber,
		};

		await _context.Agents.AddAsync(agent);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> ExistByIdAsync(string userId)
	{
		return await _context.Agents
			.AnyAsync(a => a.UserId == userId);
	}

	public async Task<int> GetAgentIdAsync(string userId)
	{
		var agent = await _context.Agents
			.FirstOrDefaultAsync(a => a.UserId == userId);

		return agent!.Id;
	}

	public async Task<bool> UserHasRents(string userId)
	{
		return await _context.Houses
			.AnyAsync(h => h.RenterId == userId);
	}

	public async Task<bool> UserWithPhonerNumberExistsAsync(string phoneNumber)
	{
		return await _context.Agents
			.AnyAsync(a => a.PhoneNumber == phoneNumber);
	}
}
