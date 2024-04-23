

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public class OrganizationDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? VatCode { get; set; }
    public int? DistrictId { get; set; }
    public string District { get; set; }
    public int? RegionId { get; set; }
    public string Region { get; set; }
}

