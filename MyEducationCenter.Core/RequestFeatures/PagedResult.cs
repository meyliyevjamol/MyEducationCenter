namespace MyEducationCenter.Core;

public static class PagedResultQueryObject
{
    public static PaginatedResult<TRow> AsPagedResult<TRow>(this IEnumerable<TRow> query, int PageSize, int Page)
    {
        return new PaginatedResult<TRow>(Page, PageSize, query.Count(), query.Skip(PageSize * (Page - 1)).Take(PageSize).AsEnumerable());
    }
    public static PaginatedResult<TRow> AsPagedResult<TRow>(this IEnumerable<TRow> query, RequestParameters options)
    {
        return new PaginatedResult<TRow>(options.Page, options.PageSize, query.Count(), query.Skip(options.PageSize * (options.Page - 1)).Take(options.PageSize).AsEnumerable());
    }

    //public static PaginatedResult<TRow> AsPagedResult<TRow>(this IQueryable<TRow> query, int PageSize, int Page, Action<TRow, long> rowNumberSetter)
    //{
    //    return new PaginatedResult<TRow>(Page, PageSize, query.Count(), query.Skip(PageSize * (Page - 1)).Take(PageSize).AsEnumerable()
    //        .Select(delegate (TRow a, int idx)
    //        {
    //            rowNumberSetter(a, idx + (Page - 1) * PageSize + 1);
    //            return a;
    //        }));
    //}
}
