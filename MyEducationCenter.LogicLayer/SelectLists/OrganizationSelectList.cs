using MyEducationCenter.Core;
using MyEducationCenter.DataLayer;

namespace MyEducationCenter.LogicLayer;

public static class OrganizationSelectList
{
    public static List<SelectList<int>> AsSelectList(this IQueryable<Organization> query)
    {
        return query
            .Where(a => a.StateId == 1)
            .Select(a => new SelectList<int>
            {
                Value = a.Id,
                Text = a.Name
            }).ToList();
    }
}
