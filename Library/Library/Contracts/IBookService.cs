using Library.Data.Models;
using Library.Models;

namespace Library.Contracts;

public interface IBookService
{
	Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

	Task<AddBookViewModel?> GetBookById(int bookId);

	Task<IEnumerable<AllBookViewModel>> GetCollectorBooksAsync(string collectorId);

	Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

	Task AddBookAsync(AddBookViewModel addBookViewModel);

	Task AttachBookToCollector(int bookId, string collectorId);

	Task DetachhBookFormCollector(int bookId, string collectorId);

	Task EditBookAsync(AddBookViewModel model, int id);
}
