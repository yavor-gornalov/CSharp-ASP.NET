using SeminarHub.Data.Models;
using SeminarHub.Models;

namespace SeminarHub.Services.Contracts;

public interface ISeminarService
{

	Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync();

	Task<ICollection<SeminarAllViewModel>> GetUserSeminarsAsync(string userId);

	Task<ICollection<SeminarCategoryViewModel>> GetSeminarCategoriesAsync();

	Task AddSeminarAsync(SeminarAddViewModel model, string organizerId);

	Task JoinSeminarAsync(int seminarId, string userId);

	Task LeaveSeminarAsync(int seminarId, string userId);

	Task<SeminarDetailsViewModel> GetSeminarDetailsByIdAsync(int id);

	Task<SeminarEditViewModel?> GetSeminarForEditAsync(int id);

	Task DeleteSeminarAsync(int seminarId, string userId);

	Task EditSeminarAsync(int seminarId, string userId, SeminarEditViewModel model);
}
