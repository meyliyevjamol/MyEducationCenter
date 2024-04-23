

using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleCreateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> Modules { get; set; } = new List<int>();

    public static explicit operator RoleCreateDto(Role role)
    {
        return new RoleCreateDto
        {
            Id = role.Id,
            Name = role.Name,
        };
    }

    public static explicit operator Role(RoleCreateDto role)
    {
        return new Role
        {
            Id = role.Id,
            Name = role.Name
        };
    }
}
