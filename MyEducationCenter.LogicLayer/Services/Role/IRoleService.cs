

using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public interface IRoleService
{
    PaginatedResult<RoleListDto> GetListAsync(RoleListFilterParams requestParameters);
    Task<RoleDto> GetByIdAsync(int id);
    Task<int> CreateAsync(RoleCreateDto dto);
    Task<int> UpdateAsync(RoleUpdateDto dto);
    Task DeleteAsync(int id);
}
