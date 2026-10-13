using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciasController : ControllerBase
{
    private readonly IAsistenciaService asistenciaService;

    public AsistenciasController(IAsistenciaService asistenciaService)
    {
        this.asistenciaService = asistenciaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await asistenciaService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var asistencia = await asistenciaService.GetByIdAsync(id);
        return asistencia == null ? NotFound() : Ok(asistencia);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Asistencia asistencia)
    {
        await asistenciaService.CreateAsync(asistencia);
        return CreatedAtAction(nameof(GetById), new { id = asistencia.IdAsistencia }, asistencia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Asistencia asistencia)
    {
        if (id != asistencia.IdAsistencia) return BadRequest("El ID no coincide");
        await asistenciaService.UpdateAsync(asistencia);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await asistenciaService.DeleteAsync(id);
        return NoContent();
    }
}