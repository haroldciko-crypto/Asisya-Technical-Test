using Asisya.Domain.Entities;

namespace Asisya.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category);

    Task<Category?> GetByIdAsync(int id);

    Task<IEnumerable<Category>> GetAllAsync();

    Task UpdateAsync(Category category);

    Task DeleteAsync(Category category);

    Task<bool> ExistsByNameAsync(string categoryName);
}