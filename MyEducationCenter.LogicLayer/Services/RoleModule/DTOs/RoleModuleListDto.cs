using MyEducationCenter.DataLayer;


namespace MyEducationCenter.LogicLayer;
public class RoleModuleListDto
{
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ModuleId { get; set; }
        public string Module { get; set; }
        public string Role { get; set; }

        public static explicit operator RoleModuleListDto(RoleModule roleModule)
        {
            return new RoleModuleListDto
            {
                Id = roleModule.Id,
                RoleId = roleModule.RoleId,
                ModuleId = roleModule.ModuleId,
                Role = roleModule.Role.Name,
                Module = roleModule.Module.Name,
            };
        }
}
