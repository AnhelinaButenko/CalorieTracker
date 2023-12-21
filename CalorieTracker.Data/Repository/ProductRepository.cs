using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    Task Add(Product product);

    Task<Product> GetById(int id);

    Task<Product> Update(int id, Product product);

    Task Remove(Product product);

    Task<List<Product>> GetAll();

    Task<Product> GetByName(string name);
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
}
