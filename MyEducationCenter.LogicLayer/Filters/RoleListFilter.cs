using MyEducationCenter.Core;

namespace MyEducationCenter.LogicLayer;

public static class RoleListFilter
{
    public static IQueryable<RoleListDto> FilterList(this IQueryable<RoleListDto> query, RoleListFilterParams @params)
    {
        return query;
    }
}

public class RoleListFilterParams : RequestParameters
{

}
