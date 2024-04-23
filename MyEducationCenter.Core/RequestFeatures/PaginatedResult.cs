namespace MyEducationCenter.Core;

public class PaginatedResult<T> : IPagedResult
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long Total { get; set; }
    public IEnumerable<T> Rows { get; set; } = Enumerable.Empty<T>();

    public PaginatedResult()
    {
    }

    public PaginatedResult(IPagedResult pagedResult, IEnumerable<T> rows)
        : this(pagedResult.Page, pagedResult.PageSize, pagedResult.Total, rows)
    {
    }

    public PaginatedResult(int page, int pageSize, long total, IEnumerable<T> rows)
        : this()
    {
        Page = page  == 0 ? 1 : page;
        PageSize = pageSize == 0 ? 10 : pageSize;
        Total = total;
        Rows = rows;
    }
}
