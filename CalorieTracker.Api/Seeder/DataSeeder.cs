using CalorieTracker.Data;
using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Api.Seeder;

public class DataSeeder : IDataSeeder
{
    private readonly CalorieTrackerDbContext _dbContext;

    public DataSeeder(CalorieTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        //bool isCreated = await _dbContext.Database.EnsureDeletedAsync();

        bool isCreated = await _dbContext.Database.EnsureCreatedAsync();

        if (!isCreated)
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
            return;
        }

        Manufacturer nestle = new Manufacturer
        {
            Name = "Nestle"
        };
        await _dbContext.AddAsync(nestle);
        await _dbContext.SaveChangesAsync();

        Manufacturer nestle1 = new Manufacturer
        {
            Name = "Nestle"
        };
        await _dbContext.AddAsync(nestle1);
        await _dbContext.SaveChangesAsync();

        Manufacturer mcDonalds = new Manufacturer
        {
            Name = "McDonald`s"
        };
        await _dbContext.AddAsync(mcDonalds);
        await _dbContext.SaveChangesAsync();

        // not have id yet
        Product egg = new Product
        {
            Name = "egg",
            CaloriePer100g = 153,
            ProteinPer100g = 12.7,
            FatPer100g = 11.1,
            CarbohydratePer100g = 0.6         
        };
        // got the Id
        await _dbContext.AddAsync(egg);
        await _dbContext.SaveChangesAsync();

        Product porridge = new Product
        {
            Name = "Porridge",
            CaloriePer100g = 137,
            ProteinPer100g = 4.5,
            FatPer100g = 1.6,
            CarbohydratePer100g = 27.4,
            ManufacturerId = nestle1.Id
        };
        await _dbContext.AddAsync(porridge);
        await _dbContext.SaveChangesAsync();

        Product tommato = new Product
        {
            Name = "Tommato",
            CaloriePer100g = 18,
            ProteinPer100g = 0.5,
            FatPer100g = 1.1,
            CarbohydratePer100g = 34.2
        };
        await _dbContext.AddAsync(tommato);
        await _dbContext.SaveChangesAsync();

        Product sandwich = new Product
        {
            Name = "Sandwich",
            CaloriePer100g = 180,
            ProteinPer100g = 41.5,
            FatPer100g = 21.1,
            CarbohydratePer100g = 34.2,
            ManufacturerId = mcDonalds.Id
            
        };
        await _dbContext.AddAsync(sandwich);
        await _dbContext.SaveChangesAsync();

        Product juice = new Product
        {
            Name = "Juice",
            CaloriePer100g = 39,
            ProteinPer100g = 0.9,
            FatPer100g = 0.2,
            CarbohydratePer100g = 9.2
        };
        await _dbContext.AddAsync(juice);
        await _dbContext.SaveChangesAsync();

        Product cookie = new Product
        {
            Name = "Cookie",
            CaloriePer100g = 93,
            ProteinPer100g = 1.4,
            FatPer100g = 3.2,
            CarbohydratePer100g = 119.1,
            ManufacturerId = nestle.Id
        };
        await _dbContext.AddAsync(cookie);
        await _dbContext.SaveChangesAsync();

        DailyForDay dailyFoodDairyUser1 = new DailyForDay
        {
            TotalCalories = 1750,
            TotalAmountCarbohydrates = 214.1,
            TotalAmountFats = 34,
            TotalAmountProteins = 107.4,
            Date = DateTime.UtcNow
        };
        await _dbContext.AddAsync(dailyFoodDairyUser1);
        await _dbContext.SaveChangesAsync();

        DailyForDay dailyFoodDairyUser2 = new DailyForDay
        {
            TotalCalories = 1550,
            TotalAmountCarbohydrates = 154.1,
            TotalAmountFats = 27,
            TotalAmountProteins = 86,
            Date = DateTime.UtcNow
        };
        await _dbContext.AddAsync(dailyFoodDairyUser2);
        await _dbContext.SaveChangesAsync();

        User user1 = new User
        {
            Name = "Lina",
            CurrentWeight = 59,
            DesiredWeight = 57,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(user1);
        await _dbContext.SaveChangesAsync();

        User user2 = new User
        {
            Name = "Andrew",
            CurrentWeight = 78,
            DesiredWeight = 82,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(user2);
        await _dbContext.SaveChangesAsync();

        BreakfastProduct breakfastProduct1 = new BreakfastProduct
        {
            ProductId = egg.Id,
            QuantityProduct = 3,
            TotalCalories = 160,
            TotalAmountCarbohydrates = 32.3,
            TotalAmountFats = 30.2,
            TotalAmountProteins = 45.5,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(breakfastProduct1);
        await _dbContext.SaveChangesAsync();

        BreakfastProduct breakfastProduct2 = new BreakfastProduct
        {
            ProductId = porridge.Id,
            QuantityProduct = 1,
            TotalCalories = 101.5,
            TotalAmountCarbohydrates = 56.1,
            TotalAmountFats = 3.3,
            TotalAmountProteins = 1.3,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(breakfastProduct2);
        await _dbContext.SaveChangesAsync();

        LunchProduct lunchProduct1 = new LunchProduct
        {
            ProductId = sandwich.Id,
            QuantityProduct = 2,
            TotalCalories = 203,
            TotalAmountCarbohydrates = 112.3,
            TotalAmountFats = 6.7,
            TotalAmountProteins = 2.5,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(lunchProduct1);
        await _dbContext.SaveChangesAsync();

        LunchProduct lunchProduct2 = new LunchProduct
        {
            ProductId = tommato.Id,
            QuantityProduct = 2,
            TotalCalories = 203,
            TotalAmountCarbohydrates = 112.3,
            TotalAmountFats = 6.7,
            TotalAmountProteins = 2.5,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(lunchProduct2);
        await _dbContext.SaveChangesAsync();

        DinnerProduct dinnerProduct1 = new DinnerProduct
        {
            ProductId = cookie.Id,
            QuantityProduct = 3,
            TotalCalories = 150,
            TotalAmountCarbohydrates = 214.1,
            TotalAmountFats = 5,
            TotalAmountProteins = 3.4,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(dinnerProduct1);
        await _dbContext.SaveChangesAsync();

        DinnerProduct dinnerProduct2 = new DinnerProduct
        {
            ProductId = juice.Id,
            QuantityProduct = 1,
            TotalCalories = 121,
            TotalAmountCarbohydrates = 117.6,
            TotalAmountFats = 0.2,
            TotalAmountProteins = 0.4,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(dinnerProduct2);
        await _dbContext.SaveChangesAsync();
    }
}
