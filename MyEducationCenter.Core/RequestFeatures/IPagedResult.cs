namespace MyEducationCenter.Core;

public interface IPagedResult
{
    int Page { get; }
    int PageSize { get; }
    long Total { get; }
}
