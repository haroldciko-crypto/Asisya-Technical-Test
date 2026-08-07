using Asisya.Application.DTOs.Category;

namespace Asisya.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);

    Task<CategoryResponse?> GetByIdAsync(int id);

    Task<IEnumerable<CategoryResponse>> GetAllAsync();

    Task UpdateAsync(int id, UpdateCategoryRequest request);

    Task DeleteAsync(int id);
}