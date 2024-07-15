using Library.Contracts;
using Microsoft.AspNetCore.Mvc;

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
}
