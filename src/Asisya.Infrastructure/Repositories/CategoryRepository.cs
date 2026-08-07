using Asisya.Application.Interfaces.Repositories;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Asisya.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(x => x.CategoryID == id);
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string categoryName)
    {
        return await _context.Categories
            .AnyAsync(x => x.CategoryName == categoryName);
    }
    
}