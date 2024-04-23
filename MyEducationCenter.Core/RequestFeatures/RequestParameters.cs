

namespace MyEducationCenter.Core;

public class RequestParameters
{
    const int maxPageSize = 50;
    public int Page { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value;
    }

    public virtual string? Search { get; set; } = "";

    public virtual string? SortBy { get; set; } = "ASC";              
}
