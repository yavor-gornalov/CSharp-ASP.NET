using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
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
}
