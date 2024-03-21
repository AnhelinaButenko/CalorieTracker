using CalorieTracker.Domains;
using CalorieTracker.Domains.Enums;

namespace CalorieTracker.Data.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetById(int id);

    Task<List<User>> GetAll();

    Task<double> GetActivityCoefficient(ActivityLevel activityLevel);
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private static readonly Dictionary<ActivityLevel, double> ActivityCoefficients = new Dictionary<ActivityLevel, double>
    {
        { ActivityLevel.Minimal, 1.2 },
        { ActivityLevel.Low, 1.375 },
        { ActivityLevel.Moderate, 1.55 },
        { ActivityLevel.High, 1.725 },
        { ActivityLevel.VeryHigh, 1.9 }
    };

    public async Task<double> GetActivityCoefficient(ActivityLevel activityLevel)
    {
        if (ActivityCoefficients.TryGetValue(activityLevel, out double coefficient))
        {
            return coefficient;
        }
        else
        {
            throw new ArgumentException("Invalid activity level");
        }
    }

    public UserRepository(CalorieTrackerDbContext dbContext) 
        : base(dbContext)
    {
    }
}