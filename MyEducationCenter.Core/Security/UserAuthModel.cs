namespace MyEducationCenter.Core;

public class UserAuthModel
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public string UserName { get; set; }
    public bool IsSuperAdmin { get; set; }
    public bool IsOrgAdmin { get; set; }
    public IEnumerable<string> Modules { get; set; } = new List<string>();

}
