namespace LearningASPweb.Data.Repositories;

public interface IRepository<TEntity,TKey> where TEntity : class
{
    Task<List<TEntity>> ReadAllAsync();
    Task<TEntity> ReadAsync(TKey id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
    Task<bool> ExistsAsync(TKey id);
}