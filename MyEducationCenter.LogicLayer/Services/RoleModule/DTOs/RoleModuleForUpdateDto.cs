

namespace MyEducationCenter.LogicLayer;

public class RoleModuleForUpdateDto 
{
    public int RoleId { get; set; }
    public string Name { get; set; }
    public List<int> Modules { get; set; }        
}