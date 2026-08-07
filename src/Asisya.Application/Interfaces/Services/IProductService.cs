using Asisya.Application.DTOs.Common;
using Asisya.Application.DTOs.Product;

namespace Asisya.Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductResponse> CreateAsync(CreateProductRequest request);

    Task<ProductResponse?> GetByIdAsync(int id);

    Task<PagedResponse<ProductResponse>> GetPagedAsync(ProductFilterRequest filter);

    Task UpdateAsync(int id, UpdateProductRequest request);

    Task DeleteAsync(int id);
}