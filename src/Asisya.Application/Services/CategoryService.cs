using Asisya.Application.DTOs.Category;
using Asisya.Application.Interfaces.Repositories;
using Asisya.Application.Interfaces.Services;
using Asisya.Domain.Entities;

namespace Asisya.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var exists = await _categoryRepository.ExistsByNameAsync(request.CategoryName);

        if (exists)
            throw new Exception("La categoría ya existe.");

        var category = new Category
        {
            CategoryName = request.CategoryName,
            Description = request.Description,
            Picture = request.Picture
        };

        category = await _categoryRepository.AddAsync(category);

        return new CategoryResponse
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        };
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryResponse
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        });
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            return null;

        return new CategoryResponse
        {
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName,
            Description = category.Description,
            Picture = category.Picture
        };
    }

    public async Task UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Categoría no encontrada.");

        category.CategoryName = request.CategoryName;
        category.Description = request.Description;
        category.Picture = request.Picture;

        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
            throw new Exception("Categoría no encontrada.");

        await _categoryRepository.DeleteAsync(category);
    }
}