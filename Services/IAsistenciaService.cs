using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IAsistenciaService
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<Asistencia?> GetByIdAsync(int id);
    Task CreateAsync(Asistencia asistencia);
    Task UpdateAsync(Asistencia asistencia);
    Task DeleteAsync(int id);
}