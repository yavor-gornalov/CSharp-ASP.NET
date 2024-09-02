using HouseRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Tests.Mocks;

public static class DatabaseMock
{
    public static ApplicationDbContext Instance
    {
        get
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("HouseRentingInMemoryDb" + DateTime.Now.Ticks.ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
    }
}
