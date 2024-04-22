namespace MyEducationCenter.Core;

public interface IAuthService
{
    UserAuthModel User { get; }
    EmployeeAuthModel Employee { get; }
    OrganizationAuthModel Organization { get; }
    int? RoleId { get; }
    string Login(UserForAuthenticationDto dto);
    bool IsAuthenticated { get; }
    bool HasPermission(string moduleCode);
}
