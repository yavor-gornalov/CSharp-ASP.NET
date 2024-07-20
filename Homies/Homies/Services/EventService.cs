using Homies.Contracts;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.EntityFrameworkCore;

using static Homies.Common.GlobalConstants;

namespace Homies.Services;

public class EventService : IEventService
{
	private readonly HomiesDbContext context;

	public EventService(HomiesDbContext _context)
	{
		context = _context;
	}

	public async Task AddEventAsync(AddEventViewModel eventViewModel, string organiserId)
	{
		var newEvent = new Event
		{
			Name = eventViewModel.Name,
			Description = eventViewModel.Description,
			OrganiserId = organiserId,
			CreatedOn = DateTime.Now,
			Start = eventViewModel.Start,
			End = eventViewModel.End,
			TypeId = eventViewModel.TypeId,
		};

		await context.Events.AddAsync(newEvent);
		await context.SaveChangesAsync();
	}

	public async Task<ICollection<EventViewModel>> GetAllEventsAsync()
	{
		return await context.Events
			.Select(e => new EventViewModel
			{
				Id = e.Id,
				Name = e.Name,
				Description = e.Description,
				Organiser = e.Organiser.UserName,
				Start = e.Start.ToString(DateTimeDefaultFormat),
				Type = e.Type.Name
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<ICollection<EventTypeViewModel>> GetAllEventTypesAsync()
	{
		return await context.Types
			.Select(t => new EventTypeViewModel
			{
				Id = t.Id,
				Name = t.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}
}
