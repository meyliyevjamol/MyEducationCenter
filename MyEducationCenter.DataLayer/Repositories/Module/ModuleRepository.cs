

using System.Linq.Expressions;

namespace MyEducationCenter.DataLayer;

public class ModuleRepository : GenericRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override IQueryable<Module> FindByConditionWithIncludes(Expression<Func<Module, bool>> expression, bool trackChanges, params string[] includes)
    {
        includes = new[] { "Region", "District", "State" };
        return base.FindByConditionWithIncludes(expression, trackChanges, includes);
    }
}
