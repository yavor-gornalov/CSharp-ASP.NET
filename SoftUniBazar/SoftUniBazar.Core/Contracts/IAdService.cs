using SoftUniBazar.Core.Models;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Core.Contracts;

public interface IAdService
{
    Task<IEnumerable<AdAllViewModel>> AllAsync();

    Task<int> AddAsync(AdServiceModel model, string ownerId);

    Task<Ad?> GetByIdAsync(int id);

    Task EditAsync(int id, AdServiceModel model);

    Task AddToCartAsync(int id, string userId);

    Task<ICollection<AdAllViewModel>> GetUserAdsAsync(string ownerId);
}
