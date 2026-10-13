using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class ProfesorService : IProfesorService
{
    private readonly IUnitOfWork unitOfWork;

    public ProfesorService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Profesore>> GetAllAsync() =>
        await unitOfWork.Repository<Profesore>().GetAllAsync();

    public async Task<Profesore?> GetByIdAsync(int id) =>
        await unitOfWork.Repository<Profesore>().GetByIdAsync(id);

    public async Task CreateAsync(Profesore profesor)
    {
        await unitOfWork.Repository<Profesore>().AddAsync(profesor);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Profesore profesor)
    {
        unitOfWork.Repository<Profesore>().Update(profesor);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var profesor = await unitOfWork.Repository<Profesore>().GetByIdAsync(id);
        if (profesor != null)
        {
            unitOfWork.Repository<Profesore>().Delete(profesor);
            await unitOfWork.Complete();
        }
    }
}