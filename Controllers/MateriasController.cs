using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriasController : ControllerBase
{
    private readonly IMateriaService materiaService;

    public MateriasController(IMateriaService materiaService)
    {
        this.materiaService = materiaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await materiaService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var materia = await materiaService.GetByIdAsync(id);
        return materia == null ? NotFound() : Ok(materia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Materia materia)
    {
        await materiaService.CreateAsync(materia);
        return CreatedAtAction(nameof(GetById), new { id = materia.IdMateria }, materia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Materia materia)
    {
        if (id != materia.IdMateria) return BadRequest("El ID no coincide");
        await materiaService.UpdateAsync(materia);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await materiaService.DeleteAsync(id);
        return NoContent();
    }
}