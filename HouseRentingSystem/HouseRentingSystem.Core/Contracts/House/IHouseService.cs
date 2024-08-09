using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts.House;

public interface IHouseService
{
    Task<ICollection<HouseIndexServiceModel>> LastThreeHousesAsync();

    Task<ICollection<HouseCategoryServiceModel>> AllCategoriesAsync();

    Task<bool> CategoryExistsAsync(int categoryId);

    Task<int> Create(HouseFormModel model, int agentId);
}
