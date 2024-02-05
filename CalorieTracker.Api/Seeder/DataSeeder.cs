using CalorieTracker.Data;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Api.Seeder;

public class DataSeeder : IDataSeeder
{
    private readonly CalorieTrackerDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public DataSeeder(CalorieTrackerDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        bool isCreated = await _dbContext.Database.EnsureCreatedAsync();

        if (!isCreated)
        {
            await _dbContext.Database.EnsureCreatedAsync();
            return;
        }

        Manufacturer nestle = new Manufacturer
        {
            Name = "Nestle"
        };
        await _dbContext.AddAsync(nestle);
        await _dbContext.SaveChangesAsync();

        Manufacturer mcDonalds = new Manufacturer
        {
            Name = "McDonald`s"
        };
        await _dbContext.AddAsync(mcDonalds);
        await _dbContext.SaveChangesAsync();

        Manufacturer sandora = new Manufacturer
        {
            Name = "Sandora"
        };
        await _dbContext.AddAsync(sandora);
        await _dbContext.SaveChangesAsync();

        Manufacturer silpo = new Manufacturer
        {
            Name = "Silpo"
        };
        await _dbContext.AddAsync(silpo);
        await _dbContext.SaveChangesAsync();

        Category vegetables = new Category
        {
            Name = "Vegetables"
        };
        await _dbContext.AddAsync(vegetables);
        await _dbContext.SaveChangesAsync();

        Category drinks = new Category
        {
            Name = "Drinks"
        };
        await _dbContext.AddAsync(drinks);
        await _dbContext.SaveChangesAsync();

        Category bakery = new Category
        {
            Name = "Bakery"
        };
        await _dbContext.AddAsync(bakery);
        await _dbContext.SaveChangesAsync();


        Category groceries = new Category
        {
            Name = "Groceries"
        };
        await _dbContext.AddAsync(groceries);
        await _dbContext.SaveChangesAsync();

        Category milkProducts = new Category
        {
            Name = "Milk"
        };
        await _dbContext.AddAsync(milkProducts);
        await _dbContext.SaveChangesAsync();

        Category fruits = new Category
        {
            Name = "Fruits"
        };
        await _dbContext.AddAsync(fruits);
        await _dbContext.SaveChangesAsync();

        Product egg = new Product
        {
            Name = "egg",
            CaloriePer100g = 153,
            ProteinPer100g = 12.7,
            FatPer100g = 11.1,
            CarbohydratePer100g = 0.6,
            CategoryId = milkProducts.Id
        };
        await _dbContext.AddAsync(egg);
        await _dbContext.SaveChangesAsync();

        Product candy = new Product
        {
            Name = "Candy",
            CaloriePer100g = 77,
            ProteinPer100g = 0.5,
            FatPer100g = 2.6,
            CarbohydratePer100g = 67,
        };
        await _dbContext.AddAsync(candy);
        await _dbContext.SaveChangesAsync();

        Product porridge = new Product
        {
            Name = "Porridge",
            CaloriePer100g = 137,
            ProteinPer100g = 4.5,
            FatPer100g = 1.6,
            CarbohydratePer100g = 27.4,
            ManufacturerId = silpo.Id,
            CategoryId = groceries.Id
        };
        await _dbContext.AddAsync(porridge);
        await _dbContext.SaveChangesAsync();

        Product tommato = new Product
        {
            Name = "Tommato",
            CaloriePer100g = 18,
            ProteinPer100g = 0.5,
            FatPer100g = 1.1,
            CarbohydratePer100g = 34.2,
            ManufacturerId = silpo.Id,
            CategoryId = vegetables.Id
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
            ManufacturerId = mcDonalds.Id,
            CategoryId = bakery.Id
        };
        await _dbContext.AddAsync(sandwich);
        await _dbContext.SaveChangesAsync();

        Product juice = new Product
        {
            Name = "Juice",
            CaloriePer100g = 39,
            ProteinPer100g = 0.9,
            FatPer100g = 0.2,
            CarbohydratePer100g = 9.2,
            ManufacturerId = sandora.Id,
            CategoryId = drinks.Id
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
            ManufacturerId = nestle.Id,
            CategoryId = bakery.Id
        };
        await _dbContext.AddAsync(cookie);
        await _dbContext.SaveChangesAsync();

        DailyForDay dailyFoodDairyUser1 = new DailyForDay
        {
            Date = DateTime.UtcNow,

        };
        await _dbContext.AddAsync(dailyFoodDairyUser1);
        await _dbContext.SaveChangesAsync();

        DailyForDay dailyFoodDairyUser2 = new DailyForDay
        {
            Date = DateTime.UtcNow,
        };
        await _dbContext.AddAsync(dailyFoodDairyUser2);
        await _dbContext.SaveChangesAsync();

        IdentityResult user1 = await _userManager.CreateAsync(new User
        {
            UserName = "Lina",
            Email = "Lina@gmail.com",
            CurrentWeight = 59,
            DesiredWeight = 57,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        });
       
        IdentityResult user2 = await _userManager.CreateAsync(new User
        {
            UserName = "Andrew",
            Email = "Andrew@gmail.com",
            CurrentWeight = 78,
            DesiredWeight = 82,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        });
       
        BreakfastProduct breakfastProduct1 = new BreakfastProduct
        {
            ProductId = egg.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(breakfastProduct1);
        await _dbContext.SaveChangesAsync();

        BreakfastProduct breakfastProduct2 = new BreakfastProduct
        {
            ProductId = porridge.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(breakfastProduct2);
        await _dbContext.SaveChangesAsync();

        LunchProduct lunchProduct1 = new LunchProduct
        {
            ProductId = sandwich.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(lunchProduct1);
        await _dbContext.SaveChangesAsync();

        LunchProduct lunchProduct2 = new LunchProduct
        {
            ProductId = tommato.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(lunchProduct2);
        await _dbContext.SaveChangesAsync();

        DinnerProduct dinnerProduct1 = new DinnerProduct
        {
            ProductId = cookie.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        await _dbContext.AddAsync(dinnerProduct1);
        await _dbContext.SaveChangesAsync();

        DinnerProduct dinnerProduct2 = new DinnerProduct
        {
            ProductId = juice.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        await _dbContext.AddAsync(dinnerProduct2);
        await _dbContext.SaveChangesAsync();
    }
}
