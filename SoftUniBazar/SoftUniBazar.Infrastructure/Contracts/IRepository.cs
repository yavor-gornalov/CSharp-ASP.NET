﻿namespace SoftUniBazar.Infrastructure.Contracts;

public interface IRepository
{
    IQueryable<T> All<T>() where T : class;

    IQueryable<T> AllAsReadOnly<T>() where T : class;

    Task AddAsync<T>(T entity) where T : class;

    Task<T?> FindAsync<T>(object id) where T : class;

    Task DeleteAsync<T>(object id) where T : class;

    Task<int> SaveChangesAsync();
}
