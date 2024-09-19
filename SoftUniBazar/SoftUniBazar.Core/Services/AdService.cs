using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Core.Contracts;
using SoftUniBazar.Core.Models;
using SoftUniBazar.Infrastructure.Contracts;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Core.Services;

public class AdService : IAdService
{
    private readonly IRepository repository;

    public AdService(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task<int> AddAsync(AdServiceModel model, string ownerId)
    {
        var ad = new Ad
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImageUrl = model.ImageUrl,
            OwnerId = ownerId,
            CategoryId = model.CategoryId,
            CreatedOn = DateTime.Now,
        };

        await repository.AddAsync(ad);
        await repository.SaveChangesAsync();

        return ad.Id;
    }

    public async Task AddToCartAsync(int id, string userId)
    {
        var ad = await repository.FindAsync<Ad>(id);

        if (ad != null && !ad.AdBuyers.Any(ab => ab.BuyerId == userId))
        {
            ad.AdBuyers.Add(new AdBuyer
            {
                AdId = id,
                BuyerId = userId
            });
            await repository.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<AdAllViewModel>> AllAsync()
    {
        return await repository.All<Ad>()
            .Select(a => new AdAllViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Price = a.Price,
                Category = a.Category.Name,
                CreatedOn = a.CreatedOn,
                Owner = a.Owner.UserName ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task EditAsync(int id, AdServiceModel model)
    {
        var ad = await repository.FindAsync<Ad>(id);

        if (ad == null)
        {
            return;
        }

        ad.Name = model.Name;
        ad.Description = model.Description;
        ad.Price = model.Price;
        ad.ImageUrl = model.ImageUrl;
        ad.CategoryId = model.CategoryId;

        await repository.SaveChangesAsync();
    }

    public async Task<Ad?> GetByIdAsync(int id)
    {
        return await repository.All<Ad>()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<AdAllViewModel>> GetUserAdsAsync(string ownerId)
    {
        return await repository.All<Ad>()
            .Where(a => a.AdBuyers.Any(ab => ab.BuyerId == ownerId))
            .Select(a => new AdAllViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ImageUrl = a.ImageUrl,
                Price = a.Price,
                Category = a.Category.Name,
                CreatedOn = a.CreatedOn,
                Owner = a.Owner.UserName ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task RemoveFromCartAsync(int adId, string ownerId)
    {
        var ad = repository.All<Ad>()
            .Include(a => a.AdBuyers)
            .Where(a => a.Id == adId)
            .FirstOrDefault();

        if (ad != null || !ad.AdBuyers.Any(ab => ab.BuyerId == ownerId))
        {
            ad.AdBuyers.Remove(ad.AdBuyers.First(ab => ab.BuyerId == ownerId));
            await repository.SaveChangesAsync();
        }
    }
}
