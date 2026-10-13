using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class EvaluacionService : IEvaluacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Evaluacione>> GetAllAsync() =>
        await _unitOfWork.Repository<Evaluacione>().GetAllAsync();

    public async Task<Evaluacione?> GetByIdAsync(int id) =>
        await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);

    public async Task CreateAsync(Evaluacione evaluacion)
    {
        await _unitOfWork.Repository<Evaluacione>().AddAsync(evaluacion);
        await _unitOfWork.Complete();
    }

    public async Task UpdateAsync(Evaluacione evaluacion)
    {
        _unitOfWork.Repository<Evaluacione>().Update(evaluacion);
        await _unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var evaluacion = await _unitOfWork.Repository<Evaluacione>().GetByIdAsync(id);
        if (evaluacion != null)
        {
            _unitOfWork.Repository<Evaluacione>().Delete(evaluacion);
            await _unitOfWork.Complete();
        }
    }
}