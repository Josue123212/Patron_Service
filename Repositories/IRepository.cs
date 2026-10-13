namespace LAB05_JosueChoquepuma.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(object id);
    Task AddAsync(TEntity entity);
    Task AddAndSaveAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}