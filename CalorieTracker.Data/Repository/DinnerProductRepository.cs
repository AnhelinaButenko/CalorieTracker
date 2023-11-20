using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDinnerProductRepository : IGenericRepository<DinnerProduct>
{
    Task Add(DinnerProduct dinnerProduct);

    Task<DinnerProduct> GetById(int id);

    Task<DinnerProduct> Update(int id, DinnerProduct dinnerProduct);

    Task Remove(DinnerProduct dinnerProduct);

    Task<List<DinnerProduct>> GetAll();
}

public class DinnerProductRepository : GenericRepository<DinnerProduct>, IDinnerProductRepository
{
    public DinnerProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}