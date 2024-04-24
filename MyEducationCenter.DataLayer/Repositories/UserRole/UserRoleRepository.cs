namespace MyEducationCenter.DataLayer;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
