using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IMealProductRepository : IGenericRepository<MealProduct>
{

}
public class MealProductRepository : GenericRepository<MealProduct>, IMealProductRepository
{
    public MealProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}