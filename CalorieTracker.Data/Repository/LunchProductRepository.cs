using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface ILunchProductRepository : IGenericRepository<LunchProduct>
{
    Task<LunchProduct> GetById(int id);

    Task<List<LunchProduct>> GetAll();
}

public class LunchProductRepository : GenericRepository<LunchProduct>, ILunchProductRepository
{
    public LunchProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}