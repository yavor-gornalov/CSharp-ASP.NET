using SoftUniBazar.Core.Models;

namespace SoftUniBazar.Core.Contracts;

public interface IAdService
{
    Task<IEnumerable<AdAllViewModel>> AllAsync();
}
