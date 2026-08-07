namespace Asisya.Application.DTOs.Product;

public class ProductFilterRequest
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? Search { get; set; }

    public int? CategoryID { get; set; }
}