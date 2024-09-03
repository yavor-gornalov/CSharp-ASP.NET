using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Services.Agent;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Tests.UnitTests;

public class AgentServiceTests : UnitTestsBase
{
    private IAgentService _agentService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _agentService = new AgentService(_data);
    }

    [Test]
    public async Task CreateAsync_ShouldAddAgentToDatabase()
    {
        var userId = "NewAgentUserId";
        var phoneNumber = "+35922222222";

        await _agentService.CreateAsync(userId, phoneNumber);

        var agentExists = await _data.Agents.AnyAsync(a => a.UserId == userId && a.PhoneNumber == phoneNumber);
        Assert.That(agentExists, Is.True);
    }

    [Test]
    public async Task ExistsByIdAsync_ShouldReturnTrue_IfAgentExists()
    {
        var result = await _agentService.ExistsByIdAsync(Agent.UserId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task ExistsByIdAsync_ShouldReturnFalse_IfAgentDoesNotExist()
    {
        var result = await _agentService.ExistsByIdAsync("NonExistentUserId");

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetAgentIdAsync_ShouldReturnCorrectId_IfAgentExists()
    {
        var resultAgentId = await _agentService.GetAgentIdAsync(Agent.UserId);

        Assert.That(resultAgentId, Is.EqualTo(Agent.Id));
    }

    [Test]
    public async Task UserHasRents_ShouldReturnTrue_IfUserHasRents()
    {
        var result = await _agentService.UserHasRents(Renter.Id);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UserHasRents_ShouldReturnFalse_IfUserHasNoRents()
    {
        var nonRenterId = "NonRenterUserId";
        var nonRenter = new ApplicationUser()
        {
            Id = nonRenterId,
            Email = "nonrenter@mail.bg",
            FirstName = "Non",
            LastName = "Renter",
        };
        _data.Users.Add(nonRenter);
        await _data.SaveChangesAsync();

        var result = await _agentService.UserHasRents(nonRenterId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task UserWithPhonerNumberExistsAsync_ShouldReturnTrue_IfPhoneNumberExists()
    {
        var result = await _agentService.UserWithPhonerNumberExistsAsync(Agent.PhoneNumber);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task UserWithPhonerNumberExistsAsync_ShouldReturnFalse_IfPhoneNumberDoesNotExist()
    {
        var result = await _agentService.UserWithPhonerNumberExistsAsync("+35933333333");

        Assert.That(result, Is.False);
    }
}
