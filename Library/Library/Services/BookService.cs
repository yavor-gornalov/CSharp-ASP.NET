using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services;

public class BookService : IBookService
{
	private readonly LibraryDbContext context;

	public BookService(LibraryDbContext _context)
	{
		context = _context;
	}

	public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
	{
		return await context.Books
			.Select(b => new AllBookViewModel
			{
				Id = b.Id,
				Author = b.Author,
				Title = b.Title,
				Description = b.Description,
				ImageUrl = b.ImageUrl,
				Rating = b.Rating,
				Category = b.Category.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}
}
