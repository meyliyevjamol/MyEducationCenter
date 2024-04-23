

using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Modules { get; set; } = new List<int>();

    public static explicit operator RoleListDto(Role role)
    {
        return new RoleListDto
        {
            Id = role.Id,
            Name = role.Name,
            Modules = role.RoleModules.Where(a => a.RoleId == role.Id).Select(a => a.Id).ToList(),
        };
    }
}
