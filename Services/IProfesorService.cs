using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IProfesorService
{
    Task<IEnumerable<Profesore>> GetAllAsync();
    Task<Profesore?> GetByIdAsync(int id);
    Task CreateAsync(Profesore profesor);
    Task UpdateAsync(Profesore profesor);
    Task DeleteAsync(int id);
}