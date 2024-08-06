using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models;
using SeminarHub.Services.Contracts;
using System.Globalization;
using static SeminarHub.Data.Common.ValidationConstants;

namespace SeminarHub.Services;

public class SeminarService : ISeminarService
{
	private readonly SeminarHubDbContext context;
	public SeminarService(SeminarHubDbContext _context)
	{
		context = _context;
	}

	public async Task AddSeminarAsync(SeminarAddViewModel model, string organizerId)
	{
		bool isDateValid = DateTime.TryParseExact(
			model.DateAndTime,
			DateTimeDefaultFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out DateTime dateAndTime);

		if (!isDateValid)
			throw new ArgumentException("Invalid date");

		var seminar = new Seminar
		{
			Topic = model.Topic,
			Details = model.Details,
			Lecturer = model.Lecturer,
			DateAndTime = dateAndTime,
			CategoryId = model.CategoryId,
			OrganizerId = organizerId,
			Duration = model.Duration,
		};

		await context.Seminars.AddAsync(seminar);
		await context.SaveChangesAsync();
	}

	public async Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync()
	{
		return await context.Seminars
			.Select(s => new SeminarAllViewModel
			{
				Id = s.Id,
				Topic = s.Topic,
				Lecturer = s.Lecturer,
				Organizer = s.Organizer.UserName,
				DateAndTime = s.DateAndTime.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
				Category = s.Category.Name
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<SeminarDetailsViewModel> GetSeminarDetailsByIdAsync(int seminarId)
	{
		return await context.Seminars
			.Select(s => new SeminarDetailsViewModel
			{
				Id = s.Id,
				Topic = s.Topic,
				Details = s.Details,
				Lecturer = s.Lecturer,
				Organizer = s.Organizer.UserName,
				Category = s.Category.Name,
				DateAndTime = s.DateAndTime.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
				Duration = s.Duration
			})
			.FirstAsync(s => s.Id == seminarId) ?? throw new ArgumentException("Invalid seminar");
	}

	public async Task<ICollection<SeminarCategoryViewModel>> GetSeminarCategoriesAsync()
	{
		return await context.Categories
			.Select(c => new SeminarCategoryViewModel
			{
				Id = c.Id,
				Name = c.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<ICollection<SeminarAllViewModel>> GetUserSeminarsAsync(string userId)
	{
		return await context.Seminars
			.Where(s => s.SeminarsParticipants.Any(p => p.ParticipantId == userId))
			.Select(s => new SeminarAllViewModel
			{
				Id = s.Id,
				Topic = s.Topic,
				Lecturer = s.Lecturer,
				Organizer = s.Organizer.UserName,
				DateAndTime = s.DateAndTime.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
				Category = s.Category.Name
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task JoinSeminarAsync(int seminarId, string userId)
	{
		var seminar = await context.Seminars
			.Include(s => s.SeminarsParticipants)
			.FirstAsync(s => s.Id == seminarId) ?? throw new ArgumentException("Seminar does not exist");

		if (seminar.SeminarsParticipants.Any(p => p.ParticipantId == userId))
			throw new ArgumentException("Already joined to this seminar");

		var participant = new SeminarParticipant
		{
			ParticipantId = userId,
			Seminar = seminar
		};
		seminar.SeminarsParticipants.Add(participant);

		await context.SaveChangesAsync();
	}

	public async Task LeaveSeminarAsync(int seminarId, string userId)
	{
		var seminar = await context.Seminars
			.Include(s => s.SeminarsParticipants)
			.FirstAsync(s => s.Id == seminarId) ?? throw new ArgumentException("Invalid seminar");

		var participant = seminar.SeminarsParticipants
			.First(sp => sp.ParticipantId == userId) ?? throw new ArgumentException("Invalid participant");

		seminar.SeminarsParticipants.Remove(participant);
		await context.SaveChangesAsync();
	}

	public async Task<SeminarEditViewModel?> GetSeminarForEditAsync(int seminarId)
	{
		return await context.Seminars
			.Where(s => s.Id == seminarId)
			.Select(s => new SeminarEditViewModel
			{
				Id = s.Id,
				Topic = s.Topic,
				Lecturer = s.Lecturer,
				Details = s.Details,
				DateAndTime = s.DateAndTime.ToString(DateTimeDefaultFormat, CultureInfo.InvariantCulture),
				OrganizerId = s.OrganizerId,
				Duration = s.Duration,
				CategoryId = s.CategoryId
			})
			.FirstOrDefaultAsync();
	}

	public async Task EditSeminarAsync(int seminarId, string userId, SeminarEditViewModel model)
	{
		bool isDateValid = DateTime.TryParseExact(
			model.DateAndTime,
			DateTimeDefaultFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.None,
			out DateTime dateAndTime);

		if (!isDateValid)
			throw new ArgumentException("Invalid date");

		var seminar = await context.Seminars
			.FirstOrDefaultAsync(s => s.Id == seminarId);

		if (seminar == null)
			throw new ArgumentException("Invalid seminar");

		if (seminar.OrganizerId != userId)
			throw new UnauthorizedAccessException("No permission to modify this record");

		seminar.Topic = model.Topic;
		seminar.Lecturer = model.Lecturer;
		seminar.Details = model.Details;
		seminar.DateAndTime = dateAndTime;
		seminar.Duration = model.Duration;
		seminar.CategoryId = model.CategoryId;

		await context.SaveChangesAsync();
	}
}
