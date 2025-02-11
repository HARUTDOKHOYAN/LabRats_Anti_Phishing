using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace LearningASPweb.Data.Repositories;

public abstract class AspTestDbRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    private readonly IMapper _mapper;
    protected   AnitPhoshingDbContext Context { get; }

    protected AspTestDbRepositoryBase(AnitPhoshingDbContext context , IMapper mapper)
    {
        _mapper = mapper;
        Context = context;
    }

    public async Task<List<TEntity>> ReadAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> ReadAsync(TKey id)
    {
        if(id is null) return null;
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey key)
    {
        var entity = await ReadAsync(key);
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await ReadAsync(id) != null;
    }
}