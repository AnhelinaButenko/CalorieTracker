using CalorieTracker.Domains;

namespace CalorieTracker.Data.Repository;

public interface IManufacturerRepository : IGenericRepository<Manufacturer>
{
    Task Add(Manufacturer manufacturer);

    Task<Manufacturer> GetById(int id);

    Task<Manufacturer> Update(int id, Manufacturer manufacturer);

    Task Remove(Manufacturer manufacturer);

    Task<List<Manufacturer>> GetAll();
}

public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
{
    public ManufacturerRepository(CalorieTrackerDbContext dbContext) : base(dbContext)
    {
    }
}