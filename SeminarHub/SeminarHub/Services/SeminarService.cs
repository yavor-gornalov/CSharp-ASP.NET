using Microsoft.EntityFrameworkCore;
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
}
