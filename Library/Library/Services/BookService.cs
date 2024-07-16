using Library.Contracts;
using Library.Data;
using Library.Data.Models;
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

	public async Task AddBookAsync(AddBookViewModel addBookViewModel)
	{
		var book = new Book
		{
			Author = addBookViewModel.Author,
			Title = addBookViewModel.Title,
			Description = addBookViewModel.Description,
			ImageUrl = addBookViewModel.Url,
			CategoryId = addBookViewModel.CategoryId,
		};

		await context.Books.AddAsync(book);
		await context.SaveChangesAsync();
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

	public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
	{
		return await context.Categories
			.Select(c => new CategoryViewModel
			{
				Id = c.Id,
				Name = c.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<IEnumerable<AllBookViewModel>> GetCollectorBooksAsync(string collectorId)
	{
		return await context.UsersBooks
			.Where(ub => ub.CollectorId == collectorId)
			.Select(b => new AllBookViewModel
			{
				Id = b.Book.Id,
				Author = b.Book.Author,
				Title = b.Book.Title,
				Description = b.Book.Description,
				ImageUrl = b.Book.ImageUrl,
				Rating = b.Book.Rating,
				Category = b.Book.Category.Name,
			})
			.AsNoTracking()
			.ToListAsync();
	}
}
