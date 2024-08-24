using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.Agent;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

using static HouseRentingSystem.Infrastructure.Common.CustomClaims;

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
            .Select(h => ProjectToServiceModel(h))
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

    public async Task<ICollection<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId)
    {
        return await _context.Houses
            .Where(h => h.AgentId == agentId)
            .Select(h => ProjectToServiceModel(h))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<HouseServiceModel>> AllHousesByUserIdAsync(string userId)
    {
        return await _context.Houses
            .Where(h => h.RenterId == userId)
            .Select(h => ProjectToServiceModel(h))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId);
    }

    public async Task<int> CreateAsync(HouseFormModel model, int agentId)
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

    public async Task<bool> ExistAsync(int id)
    {
        return await _context.Houses
            .AnyAsync(c => c.Id == id);
    }

    public async Task<HouseDetailsServiceModel> HouseDetailsByIdAsync(int id)
    {
        return await _context.Houses
            .Where(h => h.Id == id)
            .Select(h => new HouseDetailsServiceModel
            {
                Id = h.Id,
                Title = h.Title,
                Address = h.Address,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
                PricePerMonth = h.PricePerMonth,
                IsRented = h.RenterId != null,
                Category = h.Category.Name,
                Agent = new AgentServiceModel
                {
                    FullName = string.Join(" ", h.Agent.User.FirstName, h.Agent.User.LastName),
                    PhoneNumber = h.Agent.PhoneNumber,
                    Email = h.Agent.User.Email,
                }
            })
            .FirstAsync();
    }

    public async Task<ICollection<HouseIndexServiceModel>> LastThreeHousesAsync()
    {
        return await _context.Houses
            .OrderByDescending(h => h.Id)
            .Select(h => new HouseIndexServiceModel
            {
                Id = h.Id,
                Title = h.Title,
                Address = h.Address,
                ImageUrl = h.ImageUrl,
            })
            .Take(3)
            .AsNoTracking()
            .ToListAsync();
    }

    private static HouseServiceModel ProjectToServiceModel(Infrastructure.Data.Models.House house)
    {
        return new HouseServiceModel
        {
            Id = house.Id,
            Title = house.Title,
            Address = house.Address,
            ImageUrl = house.ImageUrl,
            PricePerMonth = house.PricePerMonth,
            IsRented = house.RenterId != null,
        };
    }

    public async Task EditAsync(int houseId, HouseFormModel model)
    {
        var house = await _context.Houses.FindAsync(houseId);

        if (house != null)
        {
            house.Title = model.Title;
            house.Address = model.Address;
            house.ImageUrl = model.ImageUrl;
            house.PricePerMonth = model.PricePerMonth;
            house.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> HasAgentWithIdAsync(int houseId, string userId)
    {
        var house = await _context.Houses.FindAsync(houseId);

        if (house == null)
        {
            return false;

        }

        var agent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == house.AgentId);

        if (agent == null)
        {
            return false;
        }

        return agent.UserId == userId;
    }

    public async Task<int> GetHouseCategoryAsync(int houseId)
    {
        var house = await _context.Houses.FindAsync(houseId);

        return house!.CategoryId;
    }

    public async Task DeleteAsync(int id)
    {
        var house = await _context.Houses.FindAsync(id);

        if (house != null)
        {
            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsRentedAsync(int id)
    {
        var house = await _context.Houses.FindAsync(id);

        return house!.RenterId != null;
    }

    public async Task<bool> IsRentedByUserWithIdAsync(int id, string userId)
    {
        var house = await _context.Houses.FindAsync(id);

        if (house == null)
        {
            return false;
        }

        return house!.RenterId == userId;
    }

    public async Task RentAsync(int houseId, string userId)
    {
        var house = await _context.Houses.FindAsync(houseId);

        house!.RenterId = userId;
        await _context.SaveChangesAsync();
    }

    public async Task LeaveAsync(int houseId)
    {
        var house = await _context.Houses.FindAsync(houseId);

        house!.RenterId = null;
        await _context.SaveChangesAsync();
    }
}
