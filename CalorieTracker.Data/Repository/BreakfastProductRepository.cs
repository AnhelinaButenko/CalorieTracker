using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IBreakfastProductRepository : IGenericRepository<BreakfastProduct>
{
    Task Add(BreakfastProduct breakfastProduct);

    Task<BreakfastProduct> GetById(int id);

    Task<BreakfastProduct> Update(int id, BreakfastProduct breakfastProduct);

    Task Remove(BreakfastProduct breakfastProduct);

    Task<List<BreakfastProduct>> GetAll();
}

public class BreakfastProductRepository : GenericRepository<BreakfastProduct>, IBreakfastProductRepository
{
    public BreakfastProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}