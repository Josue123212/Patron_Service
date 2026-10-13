using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class CursoService : ICursoService
{
    private readonly IUnitOfWork unitOfWork;

    public CursoService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Curso>> GetAllAsync() =>
        await unitOfWork.Repository<Curso>().GetAllAsync();

    public async Task<Curso?> GetByIdAsync(int id) =>
        await unitOfWork.Repository<Curso>().GetByIdAsync(id);

    public async Task CreateAsync(Curso curso)
    {
        await unitOfWork.Repository<Curso>().AddAsync(curso);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Curso curso)
    {
        unitOfWork.Repository<Curso>().Update(curso);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var curso = await unitOfWork.Repository<Curso>().GetByIdAsync(id);
        if (curso != null)
        {
            unitOfWork.Repository<Curso>().Delete(curso);
            await unitOfWork.Complete();
        }
    }
}