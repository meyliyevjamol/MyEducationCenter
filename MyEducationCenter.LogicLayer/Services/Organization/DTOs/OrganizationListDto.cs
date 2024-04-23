

namespace MyEducationCenter.LogicLayer;

public class OrganizationListDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public int? DistrictId { get; set; }
    public string District { get; set; }
    public int? RegionId { get; set; }
    public string Region { get; set; }
}
