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

    public async Task<int> AddAsync(AdAddViewModel model, string ownerId)
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
}
