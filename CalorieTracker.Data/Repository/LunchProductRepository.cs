using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface ILunchProductRepository : IGenericRepository<LunchProduct>
{
}

public class LunchProductRepository : GenericRepository<LunchProduct>, ILunchProductRepository
{
    public LunchProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}