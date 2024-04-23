using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEducationCenter.DataLayer;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override IQueryable<Role> FindByConditionWithIncludes(Expression<Func<Role, bool>> expression, bool trackChanges, params string[] includes)
    {
       // includes = new[] { "RoleModule","Module"};
        return base.FindByConditionWithIncludes(expression, trackChanges, includes);
    }
}
