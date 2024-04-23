

using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class RoleUpdateDto : RoleCreateDto
{
    public static explicit operator RoleUpdateDto(Role role)
    {
        return new RoleUpdateDto
        {
            Id = role.Id,
            Name = role.Name
        };
    }

    public static explicit operator Role(RoleUpdateDto role)
    {
        return new Role
        {
            Name = role.Name
        };
    }
}