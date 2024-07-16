using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Controllers;

public class BookController : BaseController
{
	private readonly IBookService bookService;

	public BookController(IBookService _bookService)
	{
		bookService = _bookService;
	}

	public async Task<IActionResult> All()
	{
		var books = await bookService.GetAllBooksAsync();

		return View(books);
	}

	public async Task<IActionResult> Mine()
	{

		var collectorId = GetUserId();

		var collectorBooks = new List<AllBookViewModel>();

		if (collectorId != null)
		{
			var books = await bookService.GetCollectorBooksAsync(collectorId);
			collectorBooks = books.ToList();
		}

		return View(collectorBooks);
	}

	[HttpGet]
	public async Task<IActionResult> Add()
	{
		var model = new AddBookViewModel();
		var categories = await bookService.GetAllCategoriesAsync();

		model.Categories = categories.ToList();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddBookViewModel model)
	{
		if (!ModelState.IsValid)
			return View(model);

		await bookService.AddBookAsync(model);

		return RedirectToAction(nameof(All));
	}

	[HttpPost]
	public async Task<IActionResult> AddToCollection(int id)
	{
		var collectorId = GetUserId();

		if (collectorId != null)
		{
			await bookService.AttachBookToCollector(id, collectorId);
		}

		return RedirectToAction(nameof(All));
	}

	[HttpPost]
	public async Task<IActionResult> RemoveFromCollection(int id)
	{
		var collectorId = GetUserId();

		if (collectorId != null)
		{
			await bookService.DetachhBookFormCollector(id, collectorId);
		}

		return RedirectToAction(nameof(Mine));
	}
}
