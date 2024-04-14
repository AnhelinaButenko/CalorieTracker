using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDailyForDayRepository : IGenericRepository<DailyForDay>
{
}

public class DailyForDayRepository : GenericRepository<DailyForDay>, IDailyForDayRepository
{
    public DailyForDayRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}