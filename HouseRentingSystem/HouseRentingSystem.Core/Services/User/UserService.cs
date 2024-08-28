using HouseRentingSystem.Core.Contracts.User;
using HouseRentingSystem.Core.Models.User;
using HouseRentingSystem.Data;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserServiceModel>> AllAsync()
    {
        return await _context.Users
            .Include(u => u.Agent)
            .AsNoTracking()
            .Select(u => new UserServiceModel
            {
                Email = u.Email,
                FullName = string.Join(" ", u.FirstName, u.LastName),
                PhoneNumber = u.Agent != null ? u.Agent.PhoneNumber : null,
                IsAgent = u.Agent != null
            })
            .ToListAsync();
    }
}
