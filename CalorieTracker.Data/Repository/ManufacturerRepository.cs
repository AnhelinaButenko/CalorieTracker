using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IManufacturerRepository : IGenericRepository<Manufacturer>
{
    Task<Manufacturer> GetById(int id);

    Task<Manufacturer> GetByName(string name);

    Task<List<Manufacturer>> GetAll();

    Task<List<Manufacturer>> GetAllFiltered(string? searchStr, string filter = "all");
}

public class ManufacturerRepository : GenericRepository<Manufacturer>, IManufacturerRepository
{
    protected readonly CalorieTrackerDbContext _dbContext;

    public ManufacturerRepository(CalorieTrackerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<Manufacturer> GetByName(string name)
    {
        return await _dbContext.Manufacturer
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public override async Task<Manufacturer> GetById(int id)
    {
        return await _dbContext.Manufacturer
            .Include(p => p.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<List<Manufacturer>> GetAll()
    {
        return await _dbContext.Manufacturer.Include(p => p.Products).AsNoTracking().ToListAsync();
    }

    public virtual async Task<List<Manufacturer>> GetAllFiltered(string? searchStr, string filter)
    {
        IQueryable<Manufacturer> query = _dbContext.Manufacturer.AsQueryable();

        query = filter switch
        {
            "withProducts" => query.Include(m => m.Products).Where(m => m.Products.Any()),
            "withoutProducts"=> query.Include(m => m.Products).Where(m => m.Products == null || !m.Products.Any()),
            _ => query.Include(x => x.Products),
        };

        if (!string.IsNullOrEmpty(searchStr))
        {
            query = query.Where(m => m.Name.ToLower().Contains(searchStr.ToLower()));
        }

        return await query.ToListAsync();
    }



    /*  List<Manufacturer> manufacturersList = await _repository.GetAll();

    IQueryable<Manufacturer> manufacturersQuery = manufacturersList.AsQueryable();
    IEnumerable<Manufacturer> manufacturers;

        if (filter == "withProducts")
        {
            manufacturers = manufacturersQuery.Include(m => m.Products).Where(m => m.Products.Any()).ToList();
        }
        else if (filter == "withoutProducts")
        {
            manufacturers = manufacturersQuery.Include(m => m.Products).Where(m => m.Products == null || !m.Products.Any()).ToList();
        }
        else
    {
        manufacturers = manufacturersQuery.ToList();
    }

    if (!string.IsNullOrEmpty(searchStr))
    {
        manufacturers = manufacturers.Where(m => m.Name.Contains(searchStr, StringComparison.OrdinalIgnoreCase));
    }

        List<ManufacturerDto> manufacturerDtos = manufacturers
         .Select(manufacturer => new ManufacturerDto
         {
             Id = manufacturer.Id,
             Name = manufacturer.Name,
             Products = manufacturer.Products?.Select(p => new ProductDto
             {
                 Id = p.Id,
                 Name = p.Name,
                 CaloriePer100g = p.CaloriePer100g,
                 ProteinPer100g = p.ProteinPer100g,
                 FatPer100g = p.FatPer100g,
                 CarbohydratePer100g = p.CarbohydratePer100g
             }).ToList() ?? new List<ProductDto>()
         })
         .OrderByDescending(x => x.Name)
         .ToList();

        return Ok(manufacturerDtos);*/
}
