using CalorieTracker.Domains;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;

namespace CalorieTracker.Data;

public class CalorieTrackerDbContext : IdentityDbContext<User>
{
    private readonly IConfiguration _config;

    public CalorieTrackerDbContext(IConfiguration config)
    {
        _config = config;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<DailyFoodDairy> DailyFoodDairies { get; set; }

    public DbSet<BreakfastProduct> BreakfastProducts { get; set; }

    public DbSet<LunchProduct> LunchProducts { get; set; }

    public DbSet<DinnerProduct> DinnerProducts { get; set; }


}
