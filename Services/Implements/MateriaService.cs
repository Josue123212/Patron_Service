using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class MateriaService : IMateriaService
{
    private readonly IUnitOfWork unitOfWork;

    public MateriaService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Materia>> GetAllAsync() =>
        await unitOfWork.Repository<Materia>().GetAllAsync();

    public async Task<Materia?> GetByIdAsync(int id) =>
        await unitOfWork.Repository<Materia>().GetByIdAsync(id);

    public async Task CreateAsync(Materia materia)
    {
        await unitOfWork.Repository<Materia>().AddAsync(materia);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Materia materia)
    {
        unitOfWork.Repository<Materia>().Update(materia);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var materia = await unitOfWork.Repository<Materia>().GetByIdAsync(id);
        if (materia != null)
        {
            unitOfWork.Repository<Materia>().Delete(materia);
            await unitOfWork.Complete();
        }
    }
}