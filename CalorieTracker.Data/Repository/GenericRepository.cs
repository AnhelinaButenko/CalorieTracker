using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IGenericRepository<T> 
    where T : class
{
    Task<T> Add (T entity);

    Task<T> GetById(int id);

    Task<T> Update (int id, T entity);

    Task Remove (T entity);

    Task<List<T>> GetAll ();
}

public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : class, IBaseEntity
{
    protected readonly CalorieTrackerDbContext DbContext;

    public GenericRepository(CalorieTrackerDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<T> Add(T entity)
    {
       DbContext.Set<T>().Add(entity);

       await DbContext.SaveChangesAsync();

       return entity;
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await DbContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task Remove(T entity)
    {
        DbContext.Set<T>().Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<T> Update(int id, T entity)
    {
        T entityForUpdate = await DbContext.Set<T>().SingleAsync(x => x.Id == id);

        if (entityForUpdate == null) 
        {
            return entity;
        }

        DbContext.Set<T>().Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }
}

