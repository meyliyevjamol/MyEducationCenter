using Microsoft.IdentityModel.Tokens;
using MyEducationCenter.Core;
using MyEducationCenter.LogicLayer;


namespace MyEducationCenter.LogicLayer;

public static class RoleModuleListFilter
{
    public static IQueryable<RoleModuleListDto> FilterList(this IQueryable<RoleModuleListDto> query, RoleModuleListFilterParams @params)
    {
        if (!@params.Search.IsNullOrEmpty())
            query = query.Where(a => a.Module.ToLower().Contains(@params.Search.ToLower()) ||
                                a.Role.ToLower().Contains(@params.Search.ToLower()));

        return query;
    }
}

public class RoleModuleListFilterParams : RequestParameters
{
    public int? RoleId { get; set; }
}