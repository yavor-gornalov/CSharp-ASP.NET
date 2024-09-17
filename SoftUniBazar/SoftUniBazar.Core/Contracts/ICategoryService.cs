using SoftUniBazar.Core.Models;

namespace SoftUniBazar.Core.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> AllAsync();

    Task<bool> ExistsAsync(int categoryId);
}
