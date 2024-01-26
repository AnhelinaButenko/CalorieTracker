using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task Add(Category category);

    Task<Category> GetById(int id);

    Task<Category> Update(int id, Category category);

    Task Remove(Category category);

    Task<List<Category>> GetAll();

    Task<Category> GetByName(string name);
}

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    protected readonly CalorieTrackerDbContext _dbContext;

    public CategoryRepository(CalorieTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<Category> GetByName(string name)
    {
        return await _dbContext.Category
            .FirstOrDefaultAsync(x => x.Name == name);
    }


    public override async Task<Category> GetById(int id)
    {
        return await _dbContext.Category
            .Include(p => p.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<List<Category>> GetAll()
    {
        return await _dbContext.Category.Include(p => p.Products).AsNoTracking().ToListAsync();
    }
}