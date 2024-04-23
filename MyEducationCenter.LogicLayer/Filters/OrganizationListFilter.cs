using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public static class OrganizationListFilter
{
    public static IQueryable<OrganizationListDto> FilterList(this IQueryable<OrganizationListDto> query, OrganizationListFilterParams @params)
    {
        return query;
    }
}

public class OrganizationListFilterParams : RequestParameters
{

}