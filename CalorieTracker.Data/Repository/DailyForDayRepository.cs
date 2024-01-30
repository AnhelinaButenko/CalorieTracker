using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IDailyForDayRepository : IGenericRepository<DailyForDay>
{
    Task<DailyForDay> GetById(int id);

    Task<List<DailyForDay>> GetAll();
}

public class DailyForDayRepository : GenericRepository<DailyForDay>, IDailyForDayRepository
{
    public DailyForDayRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
    }
}