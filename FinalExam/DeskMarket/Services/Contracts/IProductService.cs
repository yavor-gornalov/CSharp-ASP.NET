using DeskMarket.Models.Product;

namespace DeskMarket.Services.Contracts;

public interface IProductService
{
    Task AddProductAsync(ProductServiceModel model, string userId);
    Task AddProductToCartAsync(int productId, string userId);
    Task DeleteProductAsync(int id);
    Task EditProductAsync(ProductServiceModel model, int id);
    Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync(string userId);
    Task<IEnumerable<ProductCartViewModel>> GetClientCartProductsAsync(string userId);
    Task<ProductServiceModel?> GetProductByIdAsync(int id);
    Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id, string userId);
    Task<ProductDeleteViewModel?> GetProductForDeleteAsync(int id);
    Task<bool> IsProductBuyerAsync(int productId, string userId);
    Task<bool> IsProductExistsAsync(int id);
    Task<bool> IsProductSellerAsync(int productId, string userId);
    Task RemoveProductFromCartAsync(int productId, string userId);
}
