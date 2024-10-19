using DeskMarket.Data;
using DeskMarket.Models.Category;
using DeskMarket.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Services;

public class CategoryService(
    ApplicationDbContext context) : ICategoryService
{
    public async Task<IEnumerable<CategoryAllViewModel>> GetAllCategoriesAsync()
    {
        return await context.Categories
            .Select(c => new CategoryAllViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .AsNoTracking()
            .ToListAsync();
    }
}
