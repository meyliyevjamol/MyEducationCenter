using AutoMapper;
using MyEducationCenter.DataLayer;


namespace MyEducationCenter.LogicLayer;

public  class OrganizationConfig : Profile
{
    public OrganizationConfig()
    {
        CreateMap<Organization, OrganizationDto>();
        CreateMap<OrganizationCreateDto, Organization>();
        CreateMap<Organization, OrganizationListDto>();
    }
}
