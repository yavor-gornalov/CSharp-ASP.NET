using Library.Models;

namespace Library.Contracts;

public interface IBookService
{
	Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

	Task<IEnumerable<AllBookViewModel>> GetCollectorBooksAsync(string collectorId);

	Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();

	Task AddBookAsync(AddBookViewModel addBookViewModel);
}
