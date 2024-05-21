

using MyEducationCenter.Core;
using MyEducationCenter.LogicLayer.Services;

namespace MyEducationCenter.LogicLayer;

public static class DeletedListFilter
{
    public static IQueryable<DeletedListDto> FilterList(this IQueryable<DeletedListDto> query, DeletedListFilterParams @params)
    {
        return query;
    }
}

public class DeletedListFilterParams : RequestParameters
{

}