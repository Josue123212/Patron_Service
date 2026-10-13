using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly ICursoService cursoService;

    public CursosController(ICursoService cursoService)
    {
        this.cursoService = cursoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await cursoService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var curso = await cursoService.GetByIdAsync(id);
        return curso == null ? NotFound() : Ok(curso);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Curso curso)
    {
        await cursoService.CreateAsync(curso);
        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Curso curso)
    {
        if (id != curso.IdCurso) return BadRequest("ID no coincide");
        await cursoService.UpdateAsync(curso);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await cursoService.DeleteAsync(id);
        return NoContent();
    }
}