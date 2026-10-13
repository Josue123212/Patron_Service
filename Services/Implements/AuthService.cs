using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using LAB05_JosueChoquepuma.DTOs;
using LAB05_JosueChoquepuma.Models;
using LAB05_JosueChoquepuma.Repositories;

namespace LAB05_JosueChoquepuma.Services.Implements;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.configuration = configuration;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var usuarios = await this.unitOfWork.Repository<User>().GetAllAsync();
        
        // Validar que el nombre de usuario o el email no existan
        if (usuarios.Any(u => u.Username == dto.Username || u.Email == dto.Email))
        {
            return null;
        }

        // Crear usuario con contraseña hasheada mediante BCrypt
        var nuevoUsuario = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = string.IsNullOrEmpty(dto.Role) ? "User" : dto.Role
        };

        await this.unitOfWork.Repository<User>().AddAsync(nuevoUsuario);
        await this.unitOfWork.Complete();

        var token = GenerarJwtToken(nuevoUsuario);

        return new AuthResponseDto
        {
            Token = token,
            Username = nuevoUsuario.Username,
            Role = nuevoUsuario.Role
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var usuarios = await this.unitOfWork.Repository<User>().GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Username == dto.Username);

        // Verificar existencia y comparar hash de contraseña con BCrypt
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
        {
            return null;
        }

        var token = GenerarJwtToken(usuario);

        return new AuthResponseDto
        {
            Token = token,
            Username = usuario.Username,
            Role = usuario.Role
        };
    }

    private string GenerarJwtToken(User usuario)
    {
        var secretKey = this.configuration["JwtSettings:SecretKey"] 
            ?? "ClaveSecretaSuperSeguraParaJWT2026Lab05JosueChoquepuma!";
        var key = Encoding.UTF8.GetBytes(secretKey);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUser.ToString()),
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}