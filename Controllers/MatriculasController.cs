using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;
 
[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly IMatriculaService matriculaService;

    public MatriculasController(IMatriculaService matriculaService)
    {
        this.matriculaService = matriculaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await matriculaService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var matricula = await matriculaService.GetByIdAsync(id);
        return matricula == null ? NotFound() : Ok(matricula);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Matricula matricula)
    {
        await matriculaService.CreateAsync(matricula);
        return CreatedAtAction(nameof(GetById), new { id = matricula.IdMatricula }, matricula);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Matricula matricula)
    {
        if (id != matricula.IdMatricula) return BadRequest("El ID no coincide");
        await matriculaService.UpdateAsync(matricula);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await matriculaService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("matricular")]
    public async Task<IActionResult> Matricular(int idEstudiante, int idCurso, string semestre)
    {
        var resultado = await matriculaService.MatricularEstudianteAsync(idEstudiante, idCurso, semestre);
        
        if (!resultado) 
            return BadRequest("No se pudo completar la matrícula. Verifique el ID del estudiante o del curso.");

        return Ok(new { mensaje = "Estudiante matriculado con éxito en el curso." });
    }
}