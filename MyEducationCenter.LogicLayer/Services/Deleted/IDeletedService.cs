

using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer.Services;

public interface IDeletedService
{
    PaginatedResult<DeletedListDto> GetListAsync(DeletedListFilterParams requestParameters);
    Task<DeletedDto> GetByIdAsync(long id);
    Task<int> CreateAsync(DeletedCreateDto dto);
    Task RestoreData(long[] ids);
    void DeleteAsync(long[] ids);
}
