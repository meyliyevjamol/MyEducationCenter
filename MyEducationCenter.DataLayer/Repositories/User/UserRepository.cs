namespace MyEducationCenter.DataLayer;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
