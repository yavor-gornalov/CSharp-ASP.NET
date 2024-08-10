using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Enums;
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

    public async Task<HouseQueryServiceModel> AllAsync(
        string? category = null,
        string? searchTerm = null,
        HouseSorting sorting = HouseSorting.Newest,
        int currentPage = 1,
        int housesPerPage = 1)
    {
        var housesQuery = _context.Houses
            .AsQueryable()
            .AsNoTracking();

        if (!string.IsNullOrEmpty(category))
        {
            housesQuery = housesQuery
                .Where(h => h.Category.Name == category);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            housesQuery = housesQuery
                .Where(h =>
                    h.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Address.ToLower().Contains(searchTerm.ToLower()) ||
                    h.Description.ToLower().Contains(searchTerm.ToLower()));
        }

        housesQuery = sorting switch
        {
            HouseSorting.Price => housesQuery
                .OrderBy(h => h.PricePerMonth),

            HouseSorting.NotRentedFirst => housesQuery
                .OrderBy(h => h.RenterId != null)
                .ThenByDescending(h => h.Id),

            _ => housesQuery
                .OrderByDescending(h => h.Id)
        };

        var houses = await housesQuery
            .Skip((currentPage - 1) * housesPerPage)
            .Take(housesPerPage)
            .Select(h => new HouseServiceModel
            {
                Id = h.Id,
                Title = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl,
                IsRented = h.RenterId != null,
                PricePerMonth = h.PricePerMonth,
            })
            .ToListAsync();

        var totalHouses = housesQuery.Count();

        return new HouseQueryServiceModel
        {
            Houses = houses,
            TotalHousesCount = totalHouses
        };
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

    public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
    {
        return await _context.Categories
            .Select(c => c.Name)
            .Distinct()
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
