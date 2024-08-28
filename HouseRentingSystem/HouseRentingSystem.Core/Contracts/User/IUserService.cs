using HouseRentingSystem.Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystem.Core.Contracts.User;

public interface IUserService
{
    Task<IEnumerable<UserServiceModel>> AllAsync();
}
