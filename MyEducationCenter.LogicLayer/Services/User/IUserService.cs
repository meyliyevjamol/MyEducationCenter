
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public interface IUserService
{
    Task<User> RegisterUser(UserForRegistrationDto dto);
    Task<LoginResultDto> Login(UserForAuthenticationDto dto);
    Task ChangeLanguageAsync(int languageId);
}
