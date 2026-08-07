using Asisya.Domain.Entities;

namespace Asisya.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}