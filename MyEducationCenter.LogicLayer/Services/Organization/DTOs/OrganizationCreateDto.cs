

using AutoMapper;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class OrganizationCreateDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? VatCode { get; set; }
    public int? DistrictId { get; set; }
    public int? RegionId { get; set; }
}
