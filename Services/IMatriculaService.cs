using LAB05_JosueChoquepuma.Models;

namespace LAB05_JosueChoquepuma.Services;

public interface IMatriculaService
{
    Task<IEnumerable<Matricula>> GetAllAsync();
    Task<Matricula?> GetByIdAsync(int id);
    Task CreateAsync(Matricula matricula);
    Task UpdateAsync(Matricula matricula);
    Task DeleteAsync(int id);
    Task<bool> MatricularEstudianteAsync(int idEstudiante, int idCurso, string semestre);
}