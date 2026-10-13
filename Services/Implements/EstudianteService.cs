using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class EstudianteService : IEstudianteService
{
    private readonly IUnitOfWork unitOfWork;

    public EstudianteService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Estudiante>> GetAllAsync()
    {
        return await unitOfWork.Repository<Estudiante>().GetAllAsync();
    }

    public async Task<Estudiante?> GetByIdAsync(int id)
    {
        return await unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
    }

    public async Task CreateAsync(Estudiante estudiante)
    {
        await unitOfWork.Repository<Estudiante>().AddAsync(estudiante);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Estudiante estudiante)
    {
        unitOfWork.Repository<Estudiante>().Update(estudiante);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var estudiante = await unitOfWork.Repository<Estudiante>().GetByIdAsync(id);
        if (estudiante != null)
        {
            unitOfWork.Repository<Estudiante>().Delete(estudiante);
            await unitOfWork.Complete();
        }
    }
}