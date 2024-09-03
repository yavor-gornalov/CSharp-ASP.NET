using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Core.Services.House;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HouseRentingSystem.Tests.UnitTests
{
    public class HouseServiceTests : UnitTestsBase
    {
        private IHouseService _houseService;

        [OneTimeSetUp]
        public void SetUp()
        {
            _houseService = new HouseService(_data);
        }

        [Test]
        public async Task ExistAsync_ShouldReturnTrue_IfHouseExists()
        {
            var result = await _houseService.ExistAsync(1);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistAsync_ShouldReturnFalse_IfHouseDoesNotExist()
        {
            var result = await _houseService.ExistAsync(0);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CreateAsync_ShouldAddHouseToDatabase()
        {
            var model = new HouseFormModel
            {
                Title = "New House",
                Address = "New Address, 123 Str.",
                Description = "This is a new house description",
                ImageUrl = "https://example.com/new-house.jpg",
                PricePerMonth = 2000,
                CategoryId = 1,
            };

            var houseId = await _houseService.CreateAsync(model, Agent.Id);

            var houseExists = await _data.Houses.AnyAsync(h => h.Id == houseId);
            Assert.That(houseExists, Is.True);
        }

        [Test]
        public async Task HouseDetailsByIdAsync_ShouldReturnCorrectHouseDetails()
        {
            var houseDetails = await _houseService.HouseDetailsByIdAsync(1);
            Assert.That(houseDetails.Id, Is.EqualTo(1));
            Assert.That(houseDetails.Title, Is.EqualTo("First Test House"));
        }

        [Test]
        public async Task EditAsync_ShouldEditHouseCorrectly()
        {
            var houseForEdit = await _data.Houses.FindAsync(2);

            if (houseForEdit == null)
            {
                Assert.Fail("House not found");
            }

            var model = new HouseFormModel
            {
                Title = "Second House Edited",
                Address = houseForEdit.Address,
                Description = houseForEdit.Description,
                ImageUrl = houseForEdit.ImageUrl,
                PricePerMonth = 1500,
                CategoryId = houseForEdit.CategoryId,
            };  

            await _houseService.EditAsync(2, model);
            var updatedHouse = await _data.Houses.FindAsync(2);

            Assert.That(updatedHouse.Title, Is.EqualTo("Second House Edited"));
            Assert.That(updatedHouse.PricePerMonth, Is.EqualTo(1500));
        }

    }
}
