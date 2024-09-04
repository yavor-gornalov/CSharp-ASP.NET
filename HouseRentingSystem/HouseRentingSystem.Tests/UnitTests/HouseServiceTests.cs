using HouseRentingSystem.Core.Contracts.House;
using HouseRentingSystem.Core.Enums;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Core.Services.House;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

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
            var houseId = RentedHouse.Id;
            var result = await _houseService.ExistAsync(houseId);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ExistAsync_ShouldReturnFalse_IfHouseDoesNotExist()
        {
            var notExistingHouseId = 3;
            var result = await _houseService.ExistAsync(notExistingHouseId);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CreateAsync_ShouldAddHouseToDatabase()
        {
            var newHouse = new HouseFormModel
            {
                Title = "New House",
                Address = "New Address, 123 Str.",
                Description = "This is a new house description",
                ImageUrl = "https://example.com/new-house.jpg",
                PricePerMonth = 2000,
                CategoryId = 1,
            };
            var houseId = await _houseService.CreateAsync(newHouse, Agent.Id);

            var lastHouse = new HouseFormModel
            {
                Title = "Last House",
                Address = "Last Address, 123 Str.",
                Description = "This is a last house description",
                ImageUrl = "https://example.com/last-house.jpg",
                PricePerMonth = 1000,
                CategoryId = 3
            };
            var lastHouseId = await _houseService.CreateAsync(lastHouse, Agent.Id);

            var houseExists = await _houseService.ExistAsync(houseId);
            var lastHouseExists = await _houseService.ExistAsync(lastHouseId);

            Assert.That(houseExists, Is.True);
            Assert.That(lastHouseExists, Is.True);
        }

        [Test]
        public async Task HouseDetailsByIdAsync_ShouldReturnCorrectHouseDetails()
        {
            var houseId = NonRentedHouse.Id;
            var houseDetails = await _houseService.HouseDetailsByIdAsync(houseId);

            if (houseDetails == null)
            {
                Assert.Fail("House not found");
            }

            Assert.Multiple(() =>
            {
                Assert.That(houseDetails?.Id, Is.EqualTo(NonRentedHouse.Id));
                Assert.That(houseDetails?.Title, Is.EqualTo(NonRentedHouse.Title));
                Assert.That(houseDetails?.Address, Is.EqualTo(NonRentedHouse.Address));
            });
        }



        [Test]
        public async Task HasAgentWithIdAsync_ShouldReturnTrue_IfGivenUserIsHouseAgent()
        {
            var result = await _houseService.HasAgentWithIdAsync(2, "AgentUserId");
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task HasAgentWithIdAsync_ShouldReturnTrue_IfGivenUserIsNotHouseAgent()
        {
            var result = await _houseService.HasAgentWithIdAsync(2, "RenterUserId");
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task GetHouseCategoryAsync_ShouldReturnCorrectCategoryId()
        {
            var categoryId = await _houseService.GetHouseCategoryAsync(2);

            Assert.That(categoryId, Is.EqualTo(2));
        }

        [Test]
        public async Task EditAsync_ShouldEditHouseCorrectly()
        {
            var houseForEditId = 2;

            var houseForEdit = await _data.Houses.FindAsync(houseForEditId);

            if (houseForEdit == null)
            {
                Assert.Fail("House not found");
            }
            var model = new HouseFormModel
            {
                Title = "Second Test House Edited",
                Address = "Test Address, 202 Str. Edited",
                Description = houseForEdit.Description,
                ImageUrl = houseForEdit.ImageUrl,
                PricePerMonth = 1500,
                CategoryId = houseForEdit.CategoryId,
            };
            await _houseService.EditAsync(houseForEditId, model);

            var updatedHouseDetails = await _houseService.HouseDetailsByIdAsync(houseForEditId);
            Assert.Multiple(() =>
            {
                Assert.That(updatedHouseDetails.Title, Is.EqualTo("Second Test House Edited"));
                Assert.That(updatedHouseDetails.Address, Is.EqualTo("Test Address, 202 Str. Edited"));
                Assert.That(updatedHouseDetails.PricePerMonth, Is.EqualTo(1500));
            });
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveHouseFromDatabase()
        {
            var houseForDeleteId = 3;

            if (await _houseService.ExistAsync(houseForDeleteId) == false)
            {
                Assert.Fail("House not found");
            }

            await _houseService.DeleteAsync(3);

            var houseExists = await _data.Houses.AnyAsync(h => h.Id == houseForDeleteId);
            Assert.That(houseExists, Is.False);
        }

        [Test]
        public async Task IsRentedAsync_ShouldReturnTrue_IfHouseIsRented()
        {
            var houseId = RentedHouse.Id;
            var result = await _houseService.IsRentedAsync(houseId);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsRentedAsync_ShouldReturnFalse_IfHouseIsNotRented()
        {
            var houseId = NonRentedHouse.Id;
            var result = await _houseService.IsRentedAsync(houseId);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task IsRentedByUserWithIdAsync_ShouldReturnTrue_IfRentedByUser()
        {
            var houseId = RentedHouse.Id;
            var renterId = Renter.Id;
            var result = await _houseService.IsRentedByUserWithIdAsync(houseId, renterId);
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsRentedByUserWithIdAsync_ShouldReturnFalse_IfNotRentedByUser()
        {
            var houseId = RentedHouse.Id;
            var otherUserId = "OtherUserId";
            var result = await _houseService.IsRentedByUserWithIdAsync(houseId, otherUserId);
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task RentAsync_ShouldSetRenterId()
        {
            var houseId = NonRentedHouse.Id;
            var renterId = Renter.Id;
            await _houseService.RentAsync(houseId, renterId);

            var rentedHouse = await _data.Houses.FindAsync(houseId);
            Assert.That(rentedHouse.RenterId, Is.EqualTo(renterId));
        }

        [Test]
        public async Task LeaveAsync_ShouldClearRenterId()
        {
            var houseId = RentedHouse.Id;
            await _houseService.LeaveAsync(houseId);

            var rentedHouse = await _data.Houses.FindAsync(houseId);
            Assert.That(rentedHouse.RenterId, Is.Null);
        }

        [Test]
        public async Task AllCategoriesAsync_ShouldReturnAllCategories()
        {
            var categories = await _houseService.AllCategoriesAsync();
            Assert.That(categories.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task AllCategoriesNamesAsync_ShouldReturnAllCategoryNames()
        {
            var categoryNames = await _houseService.AllCategoriesNamesAsync();

            Assert.That(categoryNames, Contains.Item("Cottage"));
            Assert.That(categoryNames, Contains.Item("Single"));
            Assert.That(categoryNames, Contains.Item("Duplex"));
        }

        [Test]
        public async Task AllHousesByAgentIdAsync_ShouldReturnHousesOfAgent()
        {
            var agentId = Agent.Id;
            var houses = await _houseService.AllHousesByAgentIdAsync(agentId);
            Assert.That(houses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task AllHousesByUserIdAsync_ShouldReturnHousesRentedByUser()
        {
            var houses = await _houseService.AllHousesByUserIdAsync(Renter.Id);
            Assert.That(houses.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task LastThreeHousesAsync_ShouldReturnLastThreeHouses()
        {
            var allHousesCount = await _data.Houses.CountAsync();
            var lastHouses = await _houseService.LastThreeHousesAsync();

            Assert.That(allHousesCount, Is.GreaterThanOrEqualTo(3));
            Assert.That(lastHouses.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task AllAsync_ShouldReturnFilteredHousesByCategory()
        {
            var targetCategory = "Cottage";
            var result = await _houseService.AllAsync(category: targetCategory);

            Assert.That(result.Houses.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AllAsync_ShouldReturnFilteredHousesBySearchTerm()
        {
            var searchTerm = "First";
            var result = await _houseService.AllAsync(searchTerm: searchTerm);

            Assert.That(result.Houses.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task CategoryExistsAsync_ShouldReturnTrue_IfCategoryExists()
        {
            var categoryId = RentedHouse.Category.Id;
            var exists = await _houseService.CategoryExistsAsync(categoryId);
            Assert.That(exists, Is.True);
        }

        [Test]
        public async Task CategoryExistsAsync_ShouldReturnFalse_IfCategoryDoesNotExist()
        {
            var exists = await _houseService.CategoryExistsAsync(999);
            Assert.That(exists, Is.False);
        }
    }
}
