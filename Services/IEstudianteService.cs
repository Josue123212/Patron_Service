using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IEstudianteService
{
    Task<IEnumerable<Estudiante>> GetAllAsync();
    Task<Estudiante?> GetByIdAsync(int id);
    Task CreateAsync(Estudiante estudiante);
    Task UpdateAsync(Estudiante estudiante);
    Task DeleteAsync(int id);
}