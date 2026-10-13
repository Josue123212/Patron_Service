using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface ICursoService
{
    Task<IEnumerable<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task CreateAsync(Curso curso);
    Task UpdateAsync(Curso curso);
    Task DeleteAsync(int id);
}