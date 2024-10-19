using DeskMarket.Data;
using DeskMarket.Data.Models;
using DeskMarket.Models.Category;
using DeskMarket.Models.Product;
using DeskMarket.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static DeskMarket.Data.Constants.ValidationConstants;

namespace DeskMarket.Services;

public class ProductService(
    ApplicationDbContext context) : IProductService
{
    public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync(string userId)
    {
        return await context.Products
            .Where(p => p.IsDeleted == false)
            .Select(p => new ProductIndexViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsSeller = p.SellerId == userId,
                HasBought = p.ProductsClients.Any(pc => pc.ClientId == userId)
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddProductAsync(ProductServiceModel model, string userId)
    {
        var product = new Product
        {
            ProductName = model.ProductName,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.ImageUrl,
            CategoryId = model.CategoryId,
            SellerId = userId,
            AddedOn = DateTime.ParseExact(model.AddedOn, DateTimeDefaultFormat, CultureInfo.InvariantCulture)
        };

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<ProductServiceModel?> GetProductByIdAsync(int id)
    {
        return await context.Products
            .Where(p => p.Id == id && p.IsDeleted == false)
            .Select(p => new ProductServiceModel
            {
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                AddedOn = p.AddedOn.ToString(DateTimeDefaultFormat),
                SellerId = p.SellerId
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<ProductDetailsViewModel?> GetProductDetailsAsync(int id, string userId)
    {
        return await context.Products
            .Where(p => p.Id == id && p.IsDeleted == false)
            .Select(p => new ProductDetailsViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                CategoryName = p.Category.Name,
                AddedOn = p.AddedOn.ToString(DateTimeDefaultFormat),
                Seller = p.Seller.UserName ?? string.Empty,
                HasBought = p.ProductsClients.Any(pc => pc.ClientId == userId)
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<ProductDeleteViewModel?> GetProductForDeleteAsync(int id)
    {
        return await context.Products
            .Where(p => p.Id == id && p.IsDeleted == false)
            .Select(p => new ProductDeleteViewModel
            {
                Id = p.Id,
                ProductName = p.ProductName,
                SellerId = p.SellerId,
                Seller = p.Seller.UserName ?? string.Empty
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task EditProductAsync(ProductServiceModel model, int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product != null)
        {
            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.CategoryId = model.CategoryId;
            product.AddedOn = DateTime.ParseExact(model.AddedOn, DateTimeDefaultFormat, CultureInfo.InvariantCulture);

            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product != null)
        {
            product.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProductCartViewModel>> GetClientCartProductsAsync(string userId)
    {
        return await context.ProductsClients
            .Where(pc => pc.ClientId == userId && pc.Product.IsDeleted == false)
            .Select(pc => new ProductCartViewModel
            {
                Id = pc.Product.Id,
                ProductName = pc.Product.ProductName,
                Price = pc.Product.Price,
                ImageUrl = pc.Product.ImageUrl
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddProductToCartAsync(int productId, string userId)
    {
        var productClient = new ProductClient
        {
            ProductId = productId,
            ClientId = userId
        };

        await context.ProductsClients.AddAsync(productClient);
        await context.SaveChangesAsync();
    }

    public async Task RemoveProductFromCartAsync(int productId, string userId)
    {
        var productClient = await context.ProductsClients
            .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ClientId == userId);

        if (productClient != null)
        {
            context.ProductsClients.Remove(productClient);
            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsProductSellerAsync(int productId, string userId)
    {
        return await context.Products
            .AnyAsync(p => p.Id == productId && p.SellerId == userId);
    }

    public async Task<bool> IsProductBuyerAsync(int productId, string userId)
    {
        return await context.ProductsClients
            .AnyAsync(pc => pc.ProductId == productId && pc.ClientId == userId);
    }

    public async Task<bool> IsProductExistsAsync(int id)
    {
        return await context.Products.AnyAsync(p => p.Id == id && p.IsDeleted == false);
    }
}
