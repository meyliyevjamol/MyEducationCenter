

using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public interface IOrganizationService
{
    PaginatedResult<OrganizationListDto> GetListAsync(OrganizationListFilterParams requestParameters);
    Task<OrganizationDto> GetByIdAsync(int id);
    Task<int> CreateAsync(OrganizationCreateDto dto);
    Task<int> UpdateAsync(OrganizationUpdateDto dto);
    void DeleteAsync(int id);
}
