using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetById(int id);

    Task<List<User>> GetAll();
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(CalorieTrackerDbContext dbContext) 
        : base(dbContext)
    {
    }
}