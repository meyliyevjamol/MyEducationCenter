

using System.Linq.Expressions;

namespace MyEducationCenter.DataLayer;

public class RoleModuleRepository : GenericRepository<RoleModule>, IRoleModuleRepository
{
    public RoleModuleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override IQueryable<RoleModule> FindByConditionWithIncludes(Expression<Func<RoleModule, bool>> expression, bool trackChanges, params string[] includes)
    {
        includes = new[] { "Region", "District", "State" };
        return base.FindByConditionWithIncludes(expression, trackChanges, includes);
    }
}
