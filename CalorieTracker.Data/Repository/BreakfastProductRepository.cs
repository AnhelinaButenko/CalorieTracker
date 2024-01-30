using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IBreakfastProductRepository : IGenericRepository<BreakfastProduct>
{
    Task<BreakfastProduct> GetById(int id);

    Task<List<BreakfastProduct>> GetAll();
}

public class BreakfastProductRepository : GenericRepository<BreakfastProduct>, IBreakfastProductRepository
{
    public BreakfastProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}