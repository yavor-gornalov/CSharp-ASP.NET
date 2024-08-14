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

    Task<int> Create(HouseFormModel model, int agentId);

    Task<HouseQueryServiceModel> AllAsync(
        string? category = null,
        string? searchTerm = null,
        HouseSorting sorting = HouseSorting.Newest,
        int currentPage = 1,
        int housePerPage = 1);

    Task<bool> ExistAsync(int id);

    Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id);
}
