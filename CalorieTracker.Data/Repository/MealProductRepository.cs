using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CalorieTracker.Data.Repository;

public interface IMealProductRepository : IGenericRepository<MealProduct>
{
    Task<IEnumerable<MealProduct>> GetAll(Expression<Func<MealProduct, bool>> filter);
}
public class MealProductRepository : GenericRepository<MealProduct>, IMealProductRepository
{
    public MealProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }

    public virtual async Task<IEnumerable<MealProduct>> GetAll(Expression<Func<MealProduct, bool>> filter)
    {
        return await DbContext.Set<MealProduct>().Where(filter).ToListAsync();
    }
}