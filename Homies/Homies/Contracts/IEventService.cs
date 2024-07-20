using Homies.Models;
using Microsoft.AspNetCore.Identity;

namespace Homies.Contracts;

public interface IEventService
{
	Task<ICollection<EventViewModel>> GetAllEventsAsync();

	Task<ICollection<EventTypeViewModel>> GetAllEventTypesAsync();

	Task AddEventAsync(AddEventViewModel eventViewModel, string organiserId);

	Task AddEventToUserCollectionAsync(int eventId, string userId);

	Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId);
}
