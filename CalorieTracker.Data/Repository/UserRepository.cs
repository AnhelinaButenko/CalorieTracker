using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    Task Add(User user);

    Task<User> GetById(int id);

    Task<User> Update(int id, User user);

    Task Remove(User user);

    Task<List<User>> GetAll();
}

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(CalorieTrackerDbContext dbContext) 
        : base(dbContext)
    {
    }
}