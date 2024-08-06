using SeminarHub.Models;

namespace SeminarHub.Services.Contracts;

public interface ISeminarService
{
	Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync();
	Task<ICollection<SeminarCategoryViewModel>> GetSeminarCategoriesAsync();
	Task AddSeminarAsync(SeminarAddViewModel model, string organizerId);
}
