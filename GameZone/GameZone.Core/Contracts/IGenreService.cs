using GameZone.Core.Models;

namespace GameZone.Core.Contracts;

public interface IGenreService
{
	Task<ICollection<GenreViewModel>> GetAllGenresAsync();

}
