using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IDailyForDayRepository : IGenericRepository<DailyForDay>
{
    Task<DailyForDay> GetDailyForDayForUser(int userId, DateTime date);
}

public class DailyForDayRepository : GenericRepository<DailyForDay>, IDailyForDayRepository
{
    private readonly CalorieTrackerDbContext _dbContext;

    public DailyForDayRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<DailyForDay> GetDailyForDayForUser(int userId, DateTime date)
    {
        return await _dbContext.DailyFoodDairies
            .Include(d => d.User)
            .Include(d => d.BreakfastProducts)
                .ThenInclude(bp => bp.Product)
            .Include(d => d.LunchProducts)
                .ThenInclude(lp => lp.Product)
            .Include(d => d.DinnerProducts)
            .ThenInclude(dp => dp.Product)
            .FirstOrDefaultAsync(d => d.UserId == userId && d.Date.Date == date.Date);
    }
}