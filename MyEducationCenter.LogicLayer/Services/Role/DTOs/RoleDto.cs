

using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Modules { get; set; } = new List<int>();

    public static explicit operator RoleDto(Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Modules = role.RoleModules.Where(a => a.RoleId == role.Id).Select(a => a.ModuleId).ToList(),
        };
    }

    public static explicit operator Role(RoleDto role)
    {
        return new Role
        {
            Id = role.Id,
            Name = role.Name,
        };
    }
}





