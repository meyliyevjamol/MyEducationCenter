using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public interface IRoleModuleService
{
    PaginatedResult<RoleModuleListDto> GetListAsync(RoleModuleListFilterParams requestParameters);
    Task<RoleModuleDto> GetByIdAsync(int id);
    IEnumerable<ModuleListDto> GetModuleListAsync(RoleModuleListFilterParams requestParameters);
    Task<List<int>> CreateAsync(RoleModuleForCreationDto dto);
    Task UpdateAsync(RoleModuleForUpdateDto dto);
    Task DeleteAsync(int id);
}
