using Xunit;
using Moq;
using Asisya.Application.Services;
using Asisya.Application.Interfaces.Repositories;
using Asisya.Domain.Entities;

namespace Asisya.UnitTests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange

        var productRepository = new Mock<IProductRepository>();
        var categoryRepository = new Mock<ICategoryRepository>();

        productRepository
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new Product
            {
                ProductID = 1,
                ProductName = "Servidor Dell",
                UnitPrice = 2500,
                UnitsInStock = 10,
                Discontinued = false,
                CategoryID = 1,
                Category = new Category
                {
                    CategoryID = 1,
                    CategoryName = "SERVIDORES",
                    Picture = "server.png"
                }
            });

        var service = new ProductService(
            productRepository.Object,
            categoryRepository.Object);

        // Act

        var result = await service.GetByIdAsync(1);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.ProductID);
        Assert.Equal("Servidor Dell", result.ProductName);
        Assert.Equal("SERVIDORES", result.CategoryName);
        Assert.Equal(2500, result.UnitPrice);
    }
}