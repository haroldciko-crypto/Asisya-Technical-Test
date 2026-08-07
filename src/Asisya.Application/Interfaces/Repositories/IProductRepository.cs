using Asisya.Application.DTOs.Common;
using Asisya.Application.DTOs.Product;
using Asisya.Domain.Entities;

namespace Asisya.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);

    Task AddRangeAsync(IEnumerable<Product> products);

    Task<Product?> GetByIdAsync(int id);

    Task<PagedResponse<Product>> GetPagedAsync(ProductFilterRequest filter);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);
}