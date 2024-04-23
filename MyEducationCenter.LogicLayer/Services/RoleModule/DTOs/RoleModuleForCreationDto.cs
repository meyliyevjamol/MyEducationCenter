
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleModuleForCreationDto
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int ModuleId { get; set; }
    public List<int> Modules { get; set; }
    
    public static explicit operator RoleModuleForCreationDto(RoleModule roleModule)
    {
        return new RoleModuleForCreationDto
        {
            Id = roleModule.Id,
            RoleId = roleModule.RoleId,
            ModuleId = roleModule.ModuleId
        };
    }

    public static explicit operator RoleModule(RoleModuleForCreationDto roleModule)
    {
        return new RoleModule
        {
            Id = roleModule.Id,
            RoleId = roleModule.RoleId,
            ModuleId = roleModule.ModuleId
        };
    }
}