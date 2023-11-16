using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface ILunchProductRepository : IGenericRepository<LunchProduct>
{
    Task Add(LunchProduct lunchProduct);

    Task<LunchProduct> GetById(int id);

    Task<LunchProduct> Update(int id, LunchProduct lunchProduct);

    Task Remove(LunchProduct lunchProduct);

    Task<List<LunchProduct>> GetAll();
}

public class LunchProductRepository : GenericRepository<LunchProduct>, ILunchProductRepository
{
    public LunchProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}