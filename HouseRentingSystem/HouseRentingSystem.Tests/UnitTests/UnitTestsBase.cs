using HouseRentingSystem.Data;
using HouseRentingSystem.Infrastructure.Data.Models;
using HouseRentingSystem.Tests.Mocks;

namespace HouseRentingSystem.Tests.UnitTests;

public class UnitTestsBase
{
    protected ApplicationDbContext _data;

    [OneTimeSetUp]
    public void Initialize()
    {
        _data = DatabaseMock.Instance;
        SeedData();
    }

    public ApplicationUser Renter { get; private set; } = null!;

    public Infrastructure.Data.Models.Agent Agent { get; private set; } = null!;

    public House House { get; private set; } = null!;

    public void SeedData()
    {
        Renter = new ApplicationUser()
        {
            Id = "RenterUserId",
            Email = "renter@mail.bg",
            FirstName = "Renter",
            LastName = "User",
        };
        _data.Users.Add(Renter);

        Agent = new Infrastructure.Data.Models.Agent()
        {
            User = new ApplicationUser()
            {
                Id = "AgentUserId",
                Email = "agent@mail.bg",
                FirstName = "Agent",
                LastName = "User",
            },
            PhoneNumber = "+35911111111",
        };
        _data.Agents.Add(Agent);

        var RentedHouse = new House()
        {
            Title = "First Test House",
            Address = "Test Address, 201 Str.",
            Description = "This is First House test description! And the description should be longer enough...",
            ImageUrl = "https://cdn.pixabay.com/photo/2016/06/24/10/47/house-1477041_960_720.jpg",
            PricePerMonth = 1000,
            Renter = Renter,
            Agent = Agent,
            Category = new Category()
            {
                Name = "Cottage",
            }
        };
        _data.Houses.Add(RentedHouse);

        var NonRentedHouse = new House()
        {
            Title = "Second Test House",
            Address = "Test Address, 202 Str.",
            Description = "This is Second House test description! And the description should be longer enough...",
            ImageUrl = "https://as1.ftcdn.net/v2/jpg/00/62/13/24/1000_F_62132429_pw8W4rc1qLlCAP9SS9pPFDZyyPJZHwpw.jpg",
            PricePerMonth = 1500,
            Agent = Agent,
            Category = new Category()
            {
                Name = "Single-Family",
            }
        };
        _data.Houses.Add(NonRentedHouse);

        _data.SaveChanges();
    }

    [OneTimeTearDown]
    public void Cleanup()
    {
        _data.Dispose();
    }
}