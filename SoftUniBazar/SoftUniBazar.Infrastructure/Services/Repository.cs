using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Infrastructure.Contracts;

namespace SoftUniBazar.Infrastructure.Services;

public class Repository : IRepository
{
    private readonly DbContext context;

    public Repository(DbContext context)
    {
        this.context = context;
    }

    private DbSet<T> DbSet<T>() where T : class
    {
        return context.Set<T>();
    }

    public async Task AddAsync<T>(T entity) where T : class
    {
        await DbSet<T>().AddAsync(entity);
    }

    public IQueryable<T> All<T>() where T : class
    {
        return DbSet<T>();
    }

    public IQueryable<T> AllAsReadOnly<T>() where T : class
    {
        return DbSet<T>().AsNoTracking();
    }

    public async Task DeleteAsync<T>(object id) where T : class
    {
        T? entity = await DbSet<T>().FindAsync(id);

        if (entity != null)
        {
            DbSet<T>().Remove(entity);
        }

    }

    public async Task<T?> FindAsync<T>(object id) where T : class
    {
        return await DbSet<T>().FindAsync(id);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

}
