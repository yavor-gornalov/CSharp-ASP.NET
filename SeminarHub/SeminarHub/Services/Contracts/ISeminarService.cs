using SeminarHub.Models;

namespace SeminarHub.Services.Contracts;

public interface ISeminarService
{
	Task<ICollection<SeminarAllViewModel>> GetAllSeminarsAsync();
}
