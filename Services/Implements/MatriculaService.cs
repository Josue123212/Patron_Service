using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class MatriculaService : IMatriculaService
{
    private readonly IUnitOfWork unitOfWork;

    public MatriculaService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Matricula>> GetAllAsync() =>
        await unitOfWork.Repository<Matricula>().GetAllAsync();

    public async Task<Matricula?> GetByIdAsync(int id) =>
        await unitOfWork.Repository<Matricula>().GetByIdAsync(id);

    public async Task CreateAsync(Matricula matricula)
    {
        await unitOfWork.Repository<Matricula>().AddAsync(matricula);
        await unitOfWork.Complete();
    }

    public async Task UpdateAsync(Matricula matricula)
    {
        unitOfWork.Repository<Matricula>().Update(matricula);
        await unitOfWork.Complete();
    }

    public async Task DeleteAsync(int id)
    {
        var matricula = await unitOfWork.Repository<Matricula>().GetByIdAsync(id);
        if (matricula != null)
        {
            unitOfWork.Repository<Matricula>().Delete(matricula);
            await unitOfWork.Complete();
        }
    }

    public async Task<bool> MatricularEstudianteAsync(int idEstudiante, int idCurso, string semestre)
    {
        var estudiante = await unitOfWork.Repository<Estudiante>().GetByIdAsync(idEstudiante);
        if (estudiante == null) return false;

        var curso = await unitOfWork.Repository<Curso>().GetByIdAsync(idCurso);
        if (curso == null) return false;

        var nuevaMatricula = new Matricula
        {
            IdEstudiante = idEstudiante,
            IdCurso = idCurso,
            Semestre = semestre
        };

        await unitOfWork.Repository<Matricula>().AddAsync(nuevaMatricula);
        return await unitOfWork.Complete() > 0;
    }
}