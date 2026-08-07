namespace Asisya.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(int userId, string username, string role);
}