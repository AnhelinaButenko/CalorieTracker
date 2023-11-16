using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDailyForDayRepository : IGenericRepository<DailyForDay>
{
    Task Add(DailyForDay dailyForDay);

    Task<DailyForDay> GetById(int id);

    Task<DailyForDay> Update(int id, DailyForDay dailyForDay);

    Task Remove(DailyForDay dailyForDay);

    Task<List<DailyForDay>> GetAll();
}

public class DailyForDayRepository : GenericRepository<DailyForDay>, IDailyForDayRepository
{
    public DailyForDayRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}