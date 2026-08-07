namespace Asisya.Application.DTOs.Product;

public class ProductResponse
{
    public int ProductID { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public bool? Discontinued { get; set; }

    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public string? CategoryPicture { get; set; }
}