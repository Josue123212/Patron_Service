using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IEvaluacionService
{
    Task<IEnumerable<Evaluacione>> GetAllAsync();
    Task<Evaluacione?> GetByIdAsync(int id);
    Task CreateAsync(Evaluacione evaluacion);
    Task UpdateAsync(Evaluacione evaluacion);
    Task DeleteAsync(int id);
}