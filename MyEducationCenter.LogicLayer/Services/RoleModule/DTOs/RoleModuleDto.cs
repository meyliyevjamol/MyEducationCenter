


using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleModuleDto
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int ModuleId { get; set; }
    public List<int> Modules { get; set; }

    public static explicit operator RoleModuleDto(RoleModule roleModule)
    {
        return new RoleModuleDto
        {
            Id = roleModule.Id,
            RoleId = roleModule.RoleId,
            ModuleId = roleModule.ModuleId
        };
    }

    public static explicit operator RoleModule(RoleModuleDto roleModule)
    {
        return new RoleModule
        {
            Id = roleModule.Id,
            RoleId = roleModule.RoleId,
            ModuleId = roleModule.ModuleId
        };
    }
}