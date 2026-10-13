using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LAB05_JosueChoquepuma.DTOs;
using LAB05_JosueChoquepuma.Services;

namespace LAB05_JosueChoquepuma.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var response = await this.authService.RegisterAsync(dto);
        if (response == null)
        {
            return BadRequest(new { mensaje = "El usuario o correo electrónico ya se encuentra registrado." });
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var response = await this.authService.LoginAsync(dto);
        if (response == null)
        {
            return Unauthorized(new { mensaje = "Nombre de usuario o contraseña incorrectos." });
        }

        return Ok(response);
    }
    [HttpGet("perfil")]
    [Authorize]
    public IActionResult ObtenerPerfil()
    {
        var username = this.User.Identity?.Name;
        return Ok(new { mensaje = $"Bienvenido a tu perfil, usuario autenticado: {username}" });
    }

    [HttpGet("admin-dashboard")]
    [Authorize(Roles = "Admin")]
    public IActionResult ObtenerPanelAdmin()
    {
        return Ok(new { mensaje = "Acceso concedido al panel de administración exclusivo para el rol Admin." });
    }
}