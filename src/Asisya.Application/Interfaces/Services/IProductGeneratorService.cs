namespace Asisya.Application.Interfaces.Services;

public interface IProductGeneratorService
{
    Task<int> GenerateAsync(int quantity);
}