using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly IEstudianteService estudianteService;

    public EstudiantesController(IEstudianteService estudianteService)
    {
        this.estudianteService = estudianteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var estudiantes = await estudianteService.GetAllAsync();
        return Ok(estudiantes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var estudiante = await estudianteService.GetByIdAsync(id);
        if (estudiante == null) return NotFound();
        return Ok(estudiante);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Estudiante estudiante)
    {
        await estudianteService.CreateAsync(estudiante);
        return CreatedAtAction(nameof(GetById), new { id = estudiante.IdEstudiante }, estudiante);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Estudiante estudiante)
    {
        if (id != estudiante.IdEstudiante) 
            return BadRequest("El ID de la URL no coincide con el de la entidad.");
            
        await estudianteService.UpdateAsync(estudiante);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var estudiante = await estudianteService.GetByIdAsync(id);
        if (estudiante == null) return NotFound();

        await estudianteService.DeleteAsync(id);
        return NoContent();
    }
}