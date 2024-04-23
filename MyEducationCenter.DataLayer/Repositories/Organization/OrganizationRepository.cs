



using System.Linq.Expressions;

namespace MyEducationCenter.DataLayer;

public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

  
    public override IQueryable<Organization> FindByConditionWithIncludes(Expression<Func<Organization, bool>> expression, bool trackChanges, params string[] includes)
    {
        includes = new[] { "Region","District","State" };
        return base.FindByConditionWithIncludes(expression, trackChanges, includes);
    }
}
