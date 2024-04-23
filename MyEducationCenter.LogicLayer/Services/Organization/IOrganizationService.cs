

using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public interface IOrganizationService
{
    PaginatedResult<OrganizationListDto> GetListAsync(OrganizationListFilterParams requestParameters);
    Task<OrganizationDto> GetByIdAsync(int id);
    Task<int> CreateAsync(OrganizationCreateDto dto);
    Task<OrganizationUpdateDto> UpdateAsync(OrganizationUpdateDto dto);
    Task DeleteAsync(int id);
}
