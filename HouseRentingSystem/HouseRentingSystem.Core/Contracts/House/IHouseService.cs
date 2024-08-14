using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts.House;

public interface IHouseService
{
    Task<ICollection<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);

    Task<ICollection<HouseServiceModel>> AllHousesByUserIdAsync(string userId);

    Task<ICollection<HouseIndexServiceModel>> LastThreeHousesAsync();

    Task<ICollection<HouseCategoryServiceModel>> AllCategoriesAsync();

    Task<IEnumerable<string>> AllCategoriesNamesAsync();

    Task<bool> CategoryExistsAsync(int categoryId);

    Task<int> CreateAsync(HouseFormModel model, int agentId);

    Task DeleteAsync(int id);

    Task EditAsync(int houseId, HouseFormModel model);

    Task<HouseQueryServiceModel> AllAsync(
        string? category = null,
        string? searchTerm = null,
        HouseSorting sorting = HouseSorting.Newest,
        int currentPage = 1,
        int housePerPage = 1);

    Task<bool> ExistAsync(int id);

    Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);

    Task<bool> HasAgentWithIdAsync(int houseId, string userId);

    Task<int> GetHouseCategoryAsync(int houseId);

    Task<bool> IsRentedAsync(int id);

    Task<bool> IsRentedByUserWithIdAsync(int id, string userId);

    Task RentAsync(int houseId, string userId);

    Task LeaveAsync(int houseId);
}
