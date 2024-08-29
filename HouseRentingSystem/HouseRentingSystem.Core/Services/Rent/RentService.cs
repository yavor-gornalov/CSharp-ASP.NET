using HouseRentingSystem.Core.Contracts.Rent;
using HouseRentingSystem.Core.Models.Rent;
using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Core.Services.Rent;

public class RentService : IRentService
{
    private ApplicationDbContext _context;

    public RentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RentServiceModel>> AllRentsAsync()
    {
        return await _context.Houses
            .Where(h => h.RenterId != null)
            .Select(h => new RentServiceModel
            {
                HouseTitle = h.Title,
                HouseImageUrl = h.ImageUrl,
                AgentFullName = string.Join(" ", h.Agent.User.FirstName, h.Agent.User.LastName),
                AgentEmail = h.Agent.User.Email,
                RenterFullName = h.Renter != null ? string.Join(" ", h.Renter.FirstName, h.Renter.LastName) : null!,
                RenterEmail = h.Renter != null ? h.Renter.Email : null!
            })
            .ToListAsync();
    }
}
