using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IProductRepository : IGenericRepository<Product>
{
    Task Add(Product product);

    Task<Product> GetById(int id);

    Task<Product> Update(int id, Product product);

    Task Remove(Product product);

    Task<List<Product>> GetAll();
}

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(CalorieTrackerDbContext dbContext) 
        : base(dbContext)
    {
    }
}
