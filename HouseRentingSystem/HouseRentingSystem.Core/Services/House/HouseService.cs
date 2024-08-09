using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.House;

public class HouseService : IHouseService
{
    private readonly ApplicationDbContext _context;

    public HouseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<HouseCategoryServiceModel>> AllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new HouseCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId);
    }

    public async Task<int> Create(HouseFormModel model, int agentId)
    {
        var house = new Infrastructure.Data.Models.House
        {
            Title = model.Title,
            Address = model.Address,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            PricePerMonth = model.PricePerMonth,
            CategoryId = model.CategoryId,
            AgentId = agentId,
        };

        await _context.Houses.AddAsync(house);
        await _context.SaveChangesAsync();

        return house.Id;
    }

    public async Task<ICollection<HouseIndexServiceModel>> LastThreeHousesAsync()
    {
        return await _context.Houses
            .OrderByDescending(h => h.Id)
            .Select(h => new HouseIndexServiceModel
            {
                Id = h.Id,
                Title = h.Title,
                ImageUrl = h.ImageUrl,
            })
            .Take(3)
            .AsNoTracking()
            .ToListAsync();
    }
}
