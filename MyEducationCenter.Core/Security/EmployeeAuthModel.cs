
namespace MyEducationCenter.Core;

public class EmployeeAuthModel
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
   // public StoreAuthModel Store { get; set; }

}
public class StoreAuthModel
{
    public int? Id { get; set; }
    public string StoreName { get; set; }
    public int? RegionId { get; set; }
    public int? DistrictId { get; set; }

}
