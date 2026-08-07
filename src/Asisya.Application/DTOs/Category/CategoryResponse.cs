namespace Asisya.Application.DTOs.Category;

public class CategoryResponse
{
    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Picture { get; set; }
}