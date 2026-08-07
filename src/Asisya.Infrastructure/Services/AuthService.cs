using Asisya.Application.DTOs.Auth;
using Asisya.Application.Interfaces.Repositories;
using Asisya.Application.Interfaces.Services;

namespace Asisya.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null)
            return null;

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
            return null;

        var token = _tokenService.GenerateToken(
            user.UserID,
            user.Username,
            user.Role);

        return new LoginResponse
        {
            Token = token,
            Username = user.Username,
            Role = user.Role
        };
    }
}