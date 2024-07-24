using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Identity;

namespace Homies.Contracts;

public interface IEventService
{
	Task<ICollection<EventViewModel>> GetAllEventsAsync();

	Task<AddEventViewModel?> GetEventByIdAsync(int eventId);
	
	Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId);

	Task<ICollection<EventTypeViewModel>> GetAllEventTypesAsync();

	Task AddEventAsync(AddEventViewModel eventViewModel, string organiserId);

	Task EditEventAsync(AddEventViewModel eventViewModel, int eventId, string userId);

	Task AddEventToUserCollectionAsync(int eventId, string userId);

	Task RemoveEventFromUserCollectionAsync(int eventId, string userId);

	Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId);
}
