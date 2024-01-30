using CalorieTracker.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CalorieTracker.Data;

public class CalorieTrackerDbContext : IdentityDbContext<User, Role, int>
{
    private readonly IConfiguration _config;

    public CalorieTrackerDbContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<DailyForDay> DailyFoodDairies { get; set; }

    public DbSet<BreakfastProduct> BreakfastProducts { get; set; }

    public DbSet<LunchProduct> LunchProducts { get; set; }

    public DbSet<DinnerProduct> DinnerProducts { get; set; }

    public DbSet<Manufacturer> Manufacturer { get; set; }

    public DbSet<FileModel> Files { get; set; }

    public DbSet<Category> Category { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config["ConnectionStrings:CalorieTrackerDb"]);

        base.OnConfiguring(optionsBuilder);
    }
}
