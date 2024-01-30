using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDinnerProductRepository : IGenericRepository<DinnerProduct>
{
    Task<DinnerProduct> GetById(int id);

    Task<List<DinnerProduct>> GetAll();
}

public class DinnerProductRepository : GenericRepository<DinnerProduct>, IDinnerProductRepository
{
    public DinnerProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}