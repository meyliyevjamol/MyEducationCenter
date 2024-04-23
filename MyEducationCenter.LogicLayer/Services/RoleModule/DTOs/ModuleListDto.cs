

namespace MyEducationCenter.LogicLayer;

public class ModuleListDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SubGroupList> SubGroups { get; set; }

}

public class SubGroupList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ModuleList> Modules { get; set; }
}

public class ModuleList
{
    public int Id { get; set; }
    public string Name { get; set; }
}