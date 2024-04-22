
using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.Logiclayer;

public static class AuthServiceExtension
{
    public static EmployeeAuthModel ConvertToDto(this Employee employee)
        => new EmployeeAuthModel
        {
            Id = employee.Id,
            EmployeeName = employee.FirstName + " " + employee.LastName,
            //Store = employee.Store.ConvertToDto(),

        };

    //public static StoreAuthModel ConvertToDto(this Store store)
    //   => new StoreAuthModel
    //   {
    //       Id = store?.Id,
    //       StoreName = store?.Name,
    //       RegionId = store?.RegionId,
    //       DistrictId = store?.DistrictId
    //   };

    public static OrganizationAuthModel ConvertToDto(this Organization organization)
      => new OrganizationAuthModel
      {
          Id = organization.Id,
          OrganizationName = organization.Name,
      };

    public static UserAuthModel ConvertToDto(this User user)
     => new UserAuthModel
     {
         Id = user.Id,
         UserName = user.UserName,
         OrganizationId = user.OrganizationId,
         IsOrgAdmin = user.UserTypeId == UserTypeIdConst.OrgAdmin,
         IsSuperAdmin = user.UserTypeId == UserTypeIdConst.SuperAdmin,
         Modules = user.RoleModules.Select(a => a.Module.Name).ToList()
     };
}
