using HouseRentingSystem.Core.Contracts.Agent;
using HouseRentingSystem.Core.Services.Agent;

namespace HouseRentingSystem.Tests.UnitTests.Agent;

public class AgentServiceTests : UnitTestsBase
{
    private IAgentService _agentService;

    [OneTimeSetUp]
    public void SetUp()
    {
        _agentService = new AgentService(_data);
    }

    [Test]
    public async Task GetAgentId_ShouldReturnCorrectId()
    {
        var resultAgentId = await _agentService.GetAgentIdAsync("AgentUserId");

        Assert.That(Convert.ToInt32(resultAgentId), Is.EqualTo(Agent.Id));
    }
}
