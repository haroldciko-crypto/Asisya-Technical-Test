using Asisya.Application.DTOs.Common;
using Asisya.Application.DTOs.Product;
using Asisya.Application.Interfaces.Repositories;
using Asisya.Application.Interfaces.Services;
using Asisya.Domain.Entities;

namespace Asisya.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryID);

        if (category == null)
            throw new Exception("La categoría no existe.");

        var product = new Product
        {
            ProductName = request.ProductName,
            SupplierID = request.SupplierID,
            CategoryID = request.CategoryID,
            QuantityPerUnit = request.QuantityPerUnit,
            UnitPrice = request.UnitPrice,
            UnitsInStock = request.UnitsInStock,
            UnitsOnOrder = request.UnitsOnOrder,
            ReorderLevel = request.ReorderLevel,
            Discontinued = request.Discontinued
        };

        product = await _productRepository.AddAsync(product);

        return new ProductResponse
        {
            ProductID = product.ProductID,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            Discontinued = product.Discontinued,
            CategoryID = category.CategoryID,
            CategoryName = category.CategoryName,
            CategoryPicture = category.Picture
        };
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return null;

        return new ProductResponse
        {
            ProductID = product.ProductID,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            Discontinued = product.Discontinued,
            CategoryID = product.CategoryID,
            CategoryName = product.Category.CategoryName,
            CategoryPicture = product.Category.Picture
        };
    }

    public async Task<PagedResponse<ProductResponse>> GetPagedAsync(ProductFilterRequest filter)
    {
        var pagedProducts = await _productRepository.GetPagedAsync(filter);

        return new PagedResponse<ProductResponse>
        {
            Items = pagedProducts.Items.Select(product => new ProductResponse
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Discontinued = product.Discontinued,
                CategoryID = product.CategoryID,
                CategoryName = product.Category.CategoryName,
                CategoryPicture = product.Category.Picture
            }),

            Page = pagedProducts.Page,
            PageSize = pagedProducts.PageSize,
            TotalRecords = pagedProducts.TotalRecords,
            TotalPages = pagedProducts.TotalPages
        };
    }

    public async Task UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Producto no encontrado.");

        product.ProductName = request.ProductName;
        product.UnitPrice = request.UnitPrice;
        product.UnitsInStock = request.UnitsInStock;

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            throw new Exception("Producto no encontrado.");

        await _productRepository.DeleteAsync(product);
    }
}