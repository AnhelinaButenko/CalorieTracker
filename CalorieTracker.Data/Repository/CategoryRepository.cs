using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetById(int id);

    Task<List<Category>> GetAll();

    Task<Category> GetByName(string name);

    Task<List<Category>> GetAllFiltered(string? searchStr, string filter = "all");
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

    public virtual async Task<List<Category>> GetAllFiltered(string? searchStr, string filter)
    {
        IQueryable<Category> query = _dbContext.Category.AsQueryable();

        query = filter switch
        {
            "withProducts" => query.Include(m => m.Products).Where(m => m.Products.Any()),
            "withoutProducts" => query.Include(m => m.Products).Where(m => m.Products == null || !m.Products.Any()),
            _ => query.Include(x => x.Products),
        };

        if (!string.IsNullOrEmpty(searchStr))
        {
            query = query.Where(m => m.Name.ToLower().Contains(searchStr.ToLower()));
        }

        return await query.ToListAsync();
    }
}