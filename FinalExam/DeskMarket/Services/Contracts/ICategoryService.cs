using DeskMarket.Models.Category;

namespace DeskMarket.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryAllViewModel>> GetAllCategoriesAsync();
    }
}
