

namespace MyEducationCenter.DataLayer.Repositories;

public class DeletedRepository : GenericRepository<Deleted>, IDeletedRepository
{
    public DeletedRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
