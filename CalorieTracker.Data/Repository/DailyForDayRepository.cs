using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IDailyForDayRepository : IGenericRepository<DailyForDay>
{
    Task<DailyForDay?> GetDailyForDayForUser(int userId, DateTime date);
}

public class DailyForDayRepository : GenericRepository<DailyForDay>, IDailyForDayRepository
{
    private readonly CalorieTrackerDbContext _dbContext;

    public DailyForDayRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<DailyForDay?> GetDailyForDayForUser(int userId, DateTime date)
    {
        return await _dbContext.DailyFoodDairies
            .Include(d => d.User)
            .Include(d => d.MealProducts)
                .ThenInclude(bp => bp.Product)
            .FirstOrDefaultAsync(d => d.UserId == userId && d.Date.Date == date.Date);
    }
}