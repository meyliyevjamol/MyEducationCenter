

using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.Logiclayer;

public class UserAccountDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public int OrganizationId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Organization { get; set; }
    public string Language { get; set; }
    public int? LanguageId { get; set; }
    public int? RoleId { get; set; }
    public string Role { get; set; }
    public List<string> Roles { get; set; } = new();
    public List<string> Modules { get; set; } = new();
    public bool IsOrgAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }

    public static explicit operator UserAccountDto(User user)
    {
        return new UserAccountDto
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
            Roles = user.UserRoles.Select(r => r.Role.Name).ToList(),
            Modules = user.UserRoles.FirstOrDefault().Role.RoleModules.Select(r => r.Module.Name).ToList(),
            IsOrgAdmin = user.UserTypeId==UserTypeIdConst.OrgAdmin,
            IsSuperAdmin = user.UserTypeId == UserTypeIdConst.SuperAdmin,

        };
    }
}

public class ModuleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AccountRoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
