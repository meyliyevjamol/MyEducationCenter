
using AutoMapper;
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class OrganizationService : IOrganizationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrganizationService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<int> CreateAsync(OrganizationCreateDto dto)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var entity = _unitOfWork.OrganizationRepository.Create(_mapper.Map<Organization>(dto));

                if (entity == null)
                    throw new Exception(ErrorConst.ProblemCreating);
                var returningContent = entity.Id;

                await transaction.CommitAsync();
                return returningContent;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    public void DeleteAsync(int id)
    {
        var entity = _unitOfWork.OrganizationRepository.GetByExpression(s => s.Id == id);
        if (entity == null)
        {
            throw new Exception(ErrorConst.NotFound<Organization>(id));
        }
        _unitOfWork.OrganizationRepository.Delete(entity);
    }
    public async Task<OrganizationDto> GetByIdAsync(int id)
    {
        var entity = _unitOfWork.OrganizationRepository.GetByExpression(s => s.Id == id);
        if(entity == null)
        {
            throw new Exception(ErrorConst.NotFound<Organization>(id));
        }
        return _mapper.Map<OrganizationDto>(entity);
    }
    public PaginatedResult<OrganizationListDto> GetListAsync(OrganizationListFilterParams requestParameters)
    {
        return _unitOfWork.OrganizationRepository
            .FindAll(false)
            .Select(_mapper.Map<OrganizationListDto>)
            .AsQueryable()
            .FilterList(requestParameters)
            .AsPagedResult(requestParameters);
    }
    public async Task<int> UpdateAsync(OrganizationUpdateDto dto)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var entity = _unitOfWork.OrganizationRepository.GetByExpression(s => s.Id == dto.Id);

                if (entity == null)
                {
                    throw new Exception(ErrorConst.NotFound<Organization>(dto.Id));
                }

                var returningContent = entity.Id;

                await transaction.CommitAsync();

                return returningContent;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
