using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesoresController : ControllerBase
{
    private readonly IProfesorService profesorService;

    public ProfesoresController(IProfesorService profesorService)
    {
        this.profesorService = profesorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await profesorService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var profesor = await profesorService.GetByIdAsync(id);
        return profesor == null ? NotFound() : Ok(profesor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Profesore profesor)
    {
        await profesorService.CreateAsync(profesor);
        return CreatedAtAction(nameof(GetById), new { id = profesor.IdProfesor }, profesor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Profesore profesor)
    {
        if (id != profesor.IdProfesor) return BadRequest("El ID no coincide");
        await profesorService.UpdateAsync(profesor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await profesorService.DeleteAsync(id);
        return NoContent();
    }
}