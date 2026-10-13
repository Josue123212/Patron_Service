using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesController : ControllerBase
{
    private readonly IEvaluacionService evaluacionService;

    public EvaluacionesController(IEvaluacionService evaluacionService)
    {
        this.evaluacionService = evaluacionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await evaluacionService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var evaluacion = await evaluacionService.GetByIdAsync(id);
        return evaluacion == null ? NotFound() : Ok(evaluacion);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Evaluacione evaluacion)
    {
        await evaluacionService.CreateAsync(evaluacion);
        return CreatedAtAction(nameof(GetById), new { id = evaluacion.IdEvaluacion }, evaluacion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Evaluacione evaluacion)
    {
        if (id != evaluacion.IdEvaluacion) return BadRequest("El ID no coincide");
        await evaluacionService.UpdateAsync(evaluacion);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await evaluacionService.DeleteAsync(id);
        return NoContent();
    }
}