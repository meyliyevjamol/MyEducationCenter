

namespace MyEducationCenter.DataLayer;

public class PlasticCardRepository : GenericRepository<PlasticCard>
{
    public PlasticCardRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

}
