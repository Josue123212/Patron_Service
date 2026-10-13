using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IMateriaService
{
    Task<IEnumerable<Materia>> GetAllAsync();
    Task<Materia?> GetByIdAsync(int id);
    Task CreateAsync(Materia materia);
    Task UpdateAsync(Materia materia);
    Task DeleteAsync(int id);
}