using Microsoft.EntityFrameworkCore;
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;
using MyEducationCenter.Logiclayer;
using System.Text.RegularExpressions;


namespace MyEducationCenter.LogicLayer;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;

    public UserService(
        IUnitOfWork repository, IAuthService authService, AppDbContext context)
    {
        _unitOfWork = repository;
        _authService = authService;
        _context = context;
    }

    public async Task<User> RegisterUser(UserForRegistrationDto dto)
    {
        bool canCommit = _unitOfWork.CurrentTransaction == null;
        var transaction = _unitOfWork.CurrentTransaction == null ? _unitOfWork.BeginTransaction() : _unitOfWork.CurrentTransaction;
        //using (var transaction = _repository.BeginTransaction())
        //{
        try
        {
            var existingUser = _context.Set<User>().Any(a => a.UserName == dto.UserName && a.StateId == 1);
            if (existingUser)
                throw new Exception("Username already exists!");
            
            var entity = _unitOfWork.UserRepository.Create((User)dto);
            try
            {
                foreach(var userRole in dto.Roles)
                {
                    _unitOfWork.UserRoleRepository.Create(new UserRole
                    {
                        UserId = entity.Id,
                        RoleId = userRole
                    });
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"There is problem with creating user roles.");
            }
            

           

            if (canCommit)
            {
                await transaction.CommitAsync();
            }
            return entity;
        }
        catch (ArgumentException ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("Invalid input: " + ex.Message);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception("User registration failed: " + ex.Message);
        }
        //}
    }

    public async Task<LoginResultDto> Login(UserForAuthenticationDto dto)
    {
        var token = _authService.Login(dto);

        var result = new LoginResultDto
        {
            Token = token,
            UserInfo = await GetUserInfo()
        };

        if (result.UserInfo.LanguageId != dto.LanguageId)
        {
            await ChangeLanguageAsync(dto.LanguageId.Value);
        }

        return result;
    }

    private bool IsValidEmail(string email)
    {
        const string emailRegexPattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(email, emailRegexPattern);
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit);
    }

    public async Task<UserAccountDto> GetUserInfo()
    {
        var user = _context.Users
            .Where(a => a.Id == _authService.User.Id)
            .Select(user => new UserAccountDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Language = user.Language.FullName,
                OrganizationId = user.OrganizationId,
                Organization = user.Organization.Name,
                FullName = user.Fullname,
                ShortName = user.Shortname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                LanguageId = user.LanguageId,
                Roles = user.UserRoles.Where(r => !r.IsDeleted).Select(r => r.Role.Name).ToList(),
                Modules = user.UserRoles.Where(r => !r.IsDeleted).FirstOrDefault().Role.RoleModules.Select(r => r.Module.Name).ToList(),
                IsOrgAdmin = user.UserTypeId == UserTypeIdConst.OrgAdmin,
                IsSuperAdmin = user.UserTypeId == UserTypeIdConst.SuperAdmin,
            })
            .FirstOrDefault();


        return user;
    }

    public async Task ChangeLanguageAsync(int languageId)
    {
        using (var transaction = _unitOfWork.BeginTransaction())
        {
            try
            {
                var existingEntity = await _unitOfWork.UserRepository.FindByConditionWithIncludes(s => s.Id == _authService.User.Id, trackChanges: true).FirstOrDefaultAsync();

                if (existingEntity == null)
                    throw new Exception(ErrorConst.NotFound<User>(_authService.User.Id));

                existingEntity.LanguageId = languageId;

                await transaction.CommitAsync();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
