using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IGenericRepository<T> 
    where T : class
{
    Task Add (T entity);

    Task<T> GetById(int id);

    Task<T> Update (int id, T entity);

    Task Remove (T entity);

    Task<List<T>> GetAll ();
}

public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : class, IBaseEntity
{
    protected readonly CalorieTrackerDbContext _dbContext;

    public GenericRepository(CalorieTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task Add(T entity)
    {
       _dbContext.Set<T>().Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await _dbContext.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public virtual async Task Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<T> Update(int id, T entity)
    {
        T entityForUpdate = await _dbContext.Set<T>().SingleAsync(x => x.Id == id);

        if (entityForUpdate == null) 
        {
            return entity;
        }

        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}

