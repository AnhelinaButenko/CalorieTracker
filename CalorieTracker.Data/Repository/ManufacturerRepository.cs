﻿using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Repository;

public interface IManufacturerRepository : IGenericRepository<Manufacturer>
{
    Task Add(Manufacturer manufacturer);

    Task<Manufacturer> GetById(int id);

    Task<Manufacturer> GetByName(string name);

    Task<Manufacturer> Update(int id, Manufacturer manufacturer);

    Task Remove(Manufacturer manufacturer);

    Task<List<Manufacturer>> GetAll();
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
}
