using Library.Models;

namespace Library.Contracts;

public interface IBookService
{
	Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();
}
