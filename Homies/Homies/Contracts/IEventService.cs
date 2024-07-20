using Homies.Models;

namespace Homies.Contracts;

public interface IEventService
{
	Task<ICollection<EventViewModel>> GetAllEventsAsync();

	Task<ICollection<EventTypeViewModel>> GetAllEventTypesAsync();

	Task AddEventAsync(AddEventViewModel eventViewModel, string organiserId);
}
