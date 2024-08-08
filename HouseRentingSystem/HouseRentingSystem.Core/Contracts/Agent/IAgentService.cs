namespace HouseRentingSystem.Core.Contracts.Agent;

public interface IAgentService
{
	Task<bool> ExistByIdAsync(string userId);

	Task<bool> UserWithPhonerNumberExistsAsync(string phoneNumber);

	Task<bool> UserHasRents(string userId);

	Task CreateAsync(string userId, string phoneNumber);
}
