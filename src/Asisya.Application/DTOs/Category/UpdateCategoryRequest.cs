namespace Asisya.Application.DTOs.Category;

public class UpdateCategoryRequest
{
    public string CategoryName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Picture { get; set; }
}