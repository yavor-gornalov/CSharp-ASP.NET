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
        var allUsers = new List<UserServiceModel>();

        var agents = await _context.Agents
            .Include(a => a.User)
            .AsNoTracking()
            .Select(a => new UserServiceModel
            {
                Email = a.User.Email,
                PhoneNumber = a.PhoneNumber,
                FullName = string.Join(" ", a.User.FirstName, a.User.LastName),
                IsAgent = a.PhoneNumber != null
            })
            .ToListAsync();

        var agentEmails = agents.Select(a => a.Email).ToList();

        var users = await _context.Users
            .Where(u => !agentEmails.Contains(u.Email))
            .Select(u => new UserServiceModel
            {
                Email = u.Email,
                FullName = string.Join(" ", u.FirstName, u.LastName),
                IsAgent = false
            })
            .ToListAsync();

        allUsers.AddRange(agents);
        allUsers.AddRange(users);

        return allUsers;
    }
}
