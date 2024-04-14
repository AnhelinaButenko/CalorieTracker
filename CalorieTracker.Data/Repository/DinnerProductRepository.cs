using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDinnerProductRepository : IGenericRepository<DinnerProduct>
{
}

public class DinnerProductRepository : GenericRepository<DinnerProduct>, IDinnerProductRepository
{
    public DinnerProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}