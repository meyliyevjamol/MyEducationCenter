using Microsoft.EntityFrameworkCore;
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitofWork;

    public RoleService(
        IUnitOfWork repository)
    {
        _unitofWork = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public PaginatedResult<RoleListDto> GetListAsync(RoleListFilterParams requestParameters)
    {
        var result = _unitofWork.RoleRepository.FindAll(trackChanges: false)
            .Select(a => new RoleListDto
            {
                Id = a.Id,
                Name = a.Name
            }).AsPagedResult(requestParameters.PageSize, requestParameters.Page);

        return result;
    }

    public async Task<RoleDto> GetByIdAsync(int id)
    {
        if (id == 0)
            return new RoleDto();

        var dto = await _unitofWork.RoleRepository.FindByConditionWithIncludes(s => s.Id == id, true).Include(s => s.RoleModules).FirstOrDefaultAsync();

        if (dto == null)
            throw new Exception(ErrorConst.NotFound<Role>(id));

        return (RoleDto)dto;
    }

    public async Task<int> CreateAsync(RoleCreateDto dto)
    {
        using (var transaction = _unitofWork.BeginTransaction())
        {
            try
            {
                var entity = _unitofWork.RoleRepository.Create((Role)dto);

                if (entity == null)
                    throw new Exception(ErrorConst.ProblemCreating);

                var returningContent = (RoleCreateDto)entity;

                await transaction.CommitAsync();
                return returningContent.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<int> UpdateAsync(RoleUpdateDto dto)
    {
        using (var transaction = _unitofWork.BeginTransaction())
        {
            try
            {
                var existingEntity =  _unitofWork.RoleRepository.GetByExpression(s => s.Id == dto.Id);

                if (existingEntity == null)
                    throw new Exception(ErrorConst.NotFound<Role>(dto.Id));

                SetEntityProperites(existingEntity, dto);

                await _unitofWork.SaveChangesAsync();
                await transaction.CommitAsync();

                return existingEntity.Id;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
    public async Task DeleteAsync(int id)
    {
        using (var transaction = _unitofWork.BeginTransaction())
        {
            try
            {
                var entity =  _unitofWork.RoleRepository.GetByExpression(s => s.Id == id);
                if (entity == null)
                    throw new Exception(ErrorConst.NotFound<Role>(id));

                _unitofWork.RoleRepository.Delete(entity);
                await _unitofWork.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private void SetEntityProperites(Role existingEntity, RoleUpdateDto dto)
    {
        existingEntity.Name = dto.Name;

        foreach (var roleModuleDto in dto.Modules)
        {
            var existingRoleModule = existingEntity.RoleModules.FirstOrDefault(rm => rm.Id == roleModuleDto);
            if (existingRoleModule == null)
            {
                var newRoleModule = new RoleModuleForCreationDto
                {
                    RoleId = existingEntity.Id,
                    ModuleId = roleModuleDto
                };

                _unitofWork.RoleModuleRepository.Create((RoleModule)newRoleModule);
            }
        }
    }
}
