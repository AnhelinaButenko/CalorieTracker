using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IBreakfastProductRepository : IGenericRepository<BreakfastProduct>
{
}

public class BreakfastProductRepository : GenericRepository<BreakfastProduct>, IBreakfastProductRepository
{
    public BreakfastProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}