namespace Asisya.Application.DTOs.Common;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = [];

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public int TotalPages { get; set; }
}