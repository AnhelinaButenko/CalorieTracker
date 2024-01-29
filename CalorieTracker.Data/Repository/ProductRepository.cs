using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CalorieTracker.Data.Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product> GetByName(string name);

    Task<List<Product>> GetAllFiltered(string? searchStr, string filter = "all");
}

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    protected readonly CalorieTrackerDbContext _dbContext;

    public ProductRepository(CalorieTrackerDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<Product> GetByName(string name)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == name);
    }

    public override async Task<List<Product>> GetAll()
    {
        return await _dbContext.Products.Include(p => p.Manufacturer).AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<Product>> GetAllFiltered(string? searchStr, string filter = "all")
    {
        IQueryable<Product> query = _dbContext.Products.AsQueryable();

        query = filter switch
        {
            "withManufacturer" => query.Where(p => p.ManufacturerId.HasValue).Include(x => x.Manufacturer),
            "withoutManufacturer" => query.Where(p => !p.ManufacturerId.HasValue),
            _ => query.Include(x => x.Manufacturer),
        };

        if (!string.IsNullOrEmpty(searchStr))
        {
            query = query.Where(p => p.Name.ToLower().Contains(searchStr.ToLower()));
        }

        return await query.OrderByDescending(x => x.CaloriePer100g).ToListAsync();
    }
}
