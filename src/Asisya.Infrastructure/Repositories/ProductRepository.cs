using Asisya.Application.DTOs.Common;
using Asisya.Application.DTOs.Product;
using Asisya.Application.Interfaces.Repositories;
using Asisya.Domain.Entities;
using Asisya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Asisya.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task AddRangeAsync(IEnumerable<Product> products)
    {
        await _context.Products.AddRangeAsync(products);

        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductID == id);
    }

    public async Task<PagedResponse<Product>> GetPagedAsync(ProductFilterRequest filter)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(p =>
                EF.Functions.ILike(
                    p.ProductName,
                    $"%{filter.Search}%"));
        }

        if (filter.CategoryID.HasValue)
        {
            query = query.Where(p =>
                p.CategoryID == filter.CategoryID.Value);
        }
        
        var totalRecords = await query.CountAsync();
        var products = await query
                .OrderBy(p => p.ProductID)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

        return new PagedResponse<Product>
        {
            Items = products,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalRecords = totalRecords,
            TotalPages = (int)Math.Ceiling(
                totalRecords / (double)filter.PageSize)
        };
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}