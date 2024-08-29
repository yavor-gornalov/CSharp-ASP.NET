using HouseRentingSystem.Core.Models.Rent;

namespace HouseRentingSystem.Core.Contracts.Rent;

public interface IRentService
{
    Task<IEnumerable<RentServiceModel>> AllRentsAsync();
}
