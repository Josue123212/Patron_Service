using LAB05_JosueChoquepuma.DTOs;

namespace LAB05_JosueChoquepuma.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}