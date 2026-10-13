using LAB05_JosueChoquepuma.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB05_JosueChoquepuma.Repositories.Implements;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly MatriculasDbContext context;
    protected readonly DbSet<TEntity> dbSet;

    public Repository(MatriculasDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await dbSet.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

    public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

    public async Task AddAndSaveAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public void Update(TEntity entity) => dbSet.Update(entity);

    public void Delete(TEntity entity) => dbSet.Remove(entity);
}