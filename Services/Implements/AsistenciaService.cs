using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class AsistenciaService : IAsistenciaService
{
    private readonly IUnitOfWork unitOfWork;

    public AsistenciaService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync() =>
        await unitOfWork.Repository<Asistencia>().GetAllAsync();

    public async Task<Asistencia?> GetByIdAsync(int id) =>
        await unitOfWork.Repository<Asistencia>().GetByIdAsync(id);

    public async Task CreateAsync(Asistencia asistencia)
    {
        await unitOfWork.Repository<Asistencia>().AddAsync(asistencia);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Asistencia asistencia)
    {
        unitOfWork.Repository<Asistencia>().Update(asistencia);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var asistencia = await unitOfWork.Repository<Asistencia>().GetByIdAsync(id);
        if (asistencia != null)
        {
            unitOfWork.Repository<Asistencia>().Delete(asistencia);
            await unitOfWork.Complete();
        }
    }
}