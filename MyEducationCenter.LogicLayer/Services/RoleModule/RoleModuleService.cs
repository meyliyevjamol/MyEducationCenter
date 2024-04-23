
using Microsoft.EntityFrameworkCore;
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;


public class RoleModuleService : IRoleModuleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;

    public RoleModuleService(
        IUnitOfWork repository,
        IAuthService authService,
        AppDbContext context)
    {
        _unitOfWork = repository ?? throw new ArgumentNullException(nameof(repository));
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _context = context;
    }

    public PaginatedResult<RoleModuleListDto> GetListAsync(RoleModuleListFilterParams requestParameters)
    {
        var result = _unitOfWork.RoleModuleRepository.FindAll(trackChanges: false)
            .Select(a => new RoleModuleListDto
            {
                Id = a.Id,
                RoleId = a.RoleId,
                ModuleId = a.ModuleId,
                Role = a.Role.Name,
                Module = a.Module.Name
            })
            .FilterList(requestParameters)
            .AsPagedResult(requestParameters.PageSize, requestParameters.Page);

        return result;
    }

    public IEnumerable<ModuleListDto> GetModuleListAsync(RoleModuleListFilterParams requestParameters)
    {
        var result = _context.Set<ModuleGroup>()
            .Select(a => new ModuleListDto
            {
                Id = a.Id,
                Name = a.Name,
                SubGroups = a.ModuleSubGroups.Select(x => new SubGroupList
                {
                    Id = x.Id,
                    Name = x.Name,
                    Modules = x.Modules.Select(m => new ModuleList
                    {
                        Id = m.Id,
                        Name = m.Name
                    }).ToList(),
                }).ToList()
            });

        return result;
    }

    public async Task<RoleModuleDto> GetByIdAsync(int id)
    {
        var dto = _unitOfWork.RoleModuleRepository.FindByConditionWithIncludes(s => s.Id == id, trackChanges: false);

        if (dto == null)
            throw new Exception(ErrorConst.NotFound<RoleModule>(id));

        return (RoleModuleDto)dto;
    }

    public async Task<List<int>> CreateAsync(RoleModuleForCreationDto dto)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var bulkObject = new List<RoleModule>();
                List<int> returnIds = new List<int> { };
                bulkObject = dto.Modules.Select(a => new RoleModule { RoleId = dto.RoleId, ModuleId = a }).ToList();

                foreach (var module in bulkObject)
                {
                    var entity = _unitOfWork.RoleModuleRepository.Create(module);
                    if (entity == null)
                        throw new Exception(ErrorConst.ProblemCreating);

                    returnIds.Add(entity.Id);


                }
                await transaction.CommitAsync();
                return returnIds;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task UpdateAsyncOld(RoleModuleForUpdateDto dto)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var roles = new List<int>();
                roles.Add(dto.RoleId);

                var existingEntity = _unitOfWork.RoleModuleRepository.FindByConditionWithIncludes(a => roles.Contains(a.RoleId), true);

                if (existingEntity == null || !existingEntity.Any())
                    throw new Exception(ErrorConst.NotFound<RoleModule>(dto.RoleId));

                SetEntityProperites(existingEntity.ToList(), dto);

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    public async Task UpdateAsync(RoleModuleForUpdateDto dto)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var existingRole = _unitOfWork.RoleRepository.FindByConditionWithIncludes(a => a.Id == dto.RoleId, true)
                                                  .FirstOrDefault();

                if (existingRole == null)
                    throw new Exception(ErrorConst.NotFound<RoleModule>(dto.RoleId));

                existingRole.Name = dto.Name;

                var existingEntities = _unitOfWork.RoleModuleRepository
                                                  .FindByConditionWithIncludes(a => a.RoleId == dto.RoleId, true)
                                                  .ToList();

                var requestedModuleIds = dto.Modules;
                var existingModuleIds = existingEntities.Select(a => a.ModuleId).ToArray();

                var toDelete = existingEntities.Where(e => !requestedModuleIds.Contains(e.ModuleId)).ToList();
                if (toDelete.Any())
                {
                    _unitOfWork.RoleModuleRepository.RemoveRange(toDelete);
                }

                var toCreate = requestedModuleIds.Where(a => !existingModuleIds.Contains(a))
                    .Select(a => new RoleModule
                    {
                        RoleId = dto.RoleId,
                        ModuleId = a
                    }).ToList();

                if (toCreate.Any())
                {
                    foreach (var module in toCreate)
                    {
                        _unitOfWork.RoleModuleRepository.Create(module);

                    }
                }

                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
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
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var entity = _unitOfWork.RoleModuleRepository.GetByExpression(s => s.Id == id);
                if (entity == null)
                    throw new Exception(ErrorConst.NotFound<RoleModule>(id));

                _unitOfWork.RoleModuleRepository.Delete(entity);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private void SetEntityProperites(List<RoleModule> existingEntity, RoleModuleForUpdateDto dto)
    {
        var moduleIdsToRemove = existingEntity
            .Where(rm => !dto.Modules.Contains(rm.ModuleId))
            .Select(rm => rm.ModuleId)
            .ToList();

        existingEntity.RemoveAll(rm => moduleIdsToRemove.Contains(rm.ModuleId));

        var newModuleIds = dto.Modules.Except(existingEntity.Select(rm => rm.ModuleId));

        foreach (var newModuleId in newModuleIds)
        {
            existingEntity.Add(new RoleModule { RoleId = dto.RoleId, ModuleId = newModuleId });
        }
    }
}

