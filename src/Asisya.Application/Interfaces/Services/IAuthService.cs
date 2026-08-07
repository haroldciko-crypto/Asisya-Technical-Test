using Asisya.Application.DTOs.Auth;

namespace Asisya.Application.Interfaces.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}