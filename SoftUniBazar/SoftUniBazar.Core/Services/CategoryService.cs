using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Core.Contracts;
using SoftUniBazar.Core.Models;
using SoftUniBazar.Infrastructure.Contracts;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Core.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository repository;

    public CategoryService(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<CategoryViewModel>> AllAsync()
    {
        return await repository.AllAsReadOnly<Category>()
            .Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int categoryId)
    {
        if (await repository.FindAsync<Category>(categoryId) != null)
            return true;
        return false;
    }
}
