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

	public async Task AddEventToUserCollectionAsync(int eventId, string userId)
	{
		var currentEvent = await context.Events
			.Include(e => e.EventsParticipants)
			.Where(e => e.Id == eventId)
			.FirstAsync();

		if (currentEvent == null)
			throw new ArgumentException("Invalid event");

		if (currentEvent.EventsParticipants.Any(ep => ep.HelperId == userId))
			throw new ArgumentException("This user already participate in this event");

		var newParticipant = new EventParticipant
		{
			EventId = eventId,
			HelperId = userId,
		};

		currentEvent.EventsParticipants.Add(newParticipant);
		await context.SaveChangesAsync();
	}

	public async Task EditEventAsync(AddEventViewModel eventViewModel, int eventId, string userId)
	{
		var eventToEdit = await context.Events
			.FindAsync(eventId);

		if (eventToEdit == null)
			throw new ArgumentException("Invalid event");

		if (eventToEdit.OrganiserId != userId)
			throw new ArgumentException("No permissions to edit this event");

		eventToEdit.Name = eventViewModel.Name;
		eventToEdit.Description = eventViewModel.Description;
		eventToEdit.Start = eventViewModel.Start;
		eventToEdit.End = eventViewModel.End;
		eventToEdit.TypeId = eventViewModel.TypeId;

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

	public async Task<AddEventViewModel?> GetEventByIdAsync(int eventId)
	{
		return await context.Events
			.Where(e => e.Id == eventId)
			.Select(e => new AddEventViewModel
			{
				Name = e.Name,
				Description = e.Description,
				Start = e.Start,
				End = e.End,
				TypeId = e.TypeId
			})
			.AsNoTracking()
			.FirstOrDefaultAsync();
	}

	public async Task<EventDetailsViewModel?> GetEventDetailsAsync(int eventId)
	{
		return await context.Events
			.Where(e => e.Id == eventId)
			.Select(e => new EventDetailsViewModel
			{
				Id = e.Id,
				Name = e.Name,
				Description = e.Description,
				Start = e.Start.ToString(DateTimeDefaultFormat),
				End = e.End.ToString(DateTimeDefaultFormat),
				CreatedOn = e.CreatedOn.ToString(DateTimeDefaultFormat),
				Organiser = e.Organiser.UserName,
				Type = e.Type.Name
			})
			.FirstOrDefaultAsync();
	}

	public async Task<ICollection<EventViewModel>> GetUserEventsAsync(string userId)
	{
		return await context.Events
			.Where(e => e.EventsParticipants.Any(ep => ep.HelperId == userId))
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

	public async Task RemoveEventFromUserCollectionAsync(int eventId, string userId)
	{
		var currentEvent = await context.Events
			.Include(e => e.EventsParticipants)
			.FirstOrDefaultAsync(e => e.Id == eventId);

		if (currentEvent == null)
			throw new ArgumentException("Invalid event");

		if (!currentEvent.EventsParticipants.Any(ep => ep.HelperId == userId))
			throw new ArgumentException("Invalid helper");

		var participant = currentEvent.EventsParticipants
			.First(ep => ep.HelperId == userId);

		currentEvent.EventsParticipants.Remove(participant);

		await context.SaveChangesAsync();
	}
}
