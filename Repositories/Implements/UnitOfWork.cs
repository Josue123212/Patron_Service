using System.Collections;
using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Repositories.Implements;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? repositories;
    private readonly MatriculasDbContext context;

    public UnitOfWork(MatriculasDbContext context)
    {
        this.context = context;
        this.repositories = new Hashtable();
    }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (repositories.ContainsKey(type))
        {
            return (IRepository<TEntity>)repositories[type]!;
        }

        var repositoryType = typeof(Repository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

        if (repositoryInstance != null)
        {
            repositories.Add(type, repositoryInstance);
            return (IRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"No se pudo instanciar el repositorio para la entidad {type}");
    }

    public void Dispose()
    {
        context.Dispose();
    }
}