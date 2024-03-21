using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Domains.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Api.Seeder;

public class DataSeeder : IDataSeeder
{
    private readonly CalorieTrackerDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILunchProductRepository _lunchProductRepository;
    private readonly IDinnerProductRepository _dinnerProductRepository;
    private readonly IDailyForDayRepository _dailyForDayRepository;
    private readonly IBreakfastProductRepository _breakfastProductRepository;

    public DataSeeder(CalorieTrackerDbContext dbContext, UserManager<User> userManager,
         IManufacturerRepository manufacturerRepository, IProductRepository productRepository, 
         ICategoryRepository categoryRepository, ILunchProductRepository lunchProductRepository,
         IDinnerProductRepository dinnerProductRepository, IDailyForDayRepository dailyForDayRepository, 
         IBreakfastProductRepository breakfastProductRepository)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _manufacturerRepository = manufacturerRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _lunchProductRepository = lunchProductRepository;
        _dinnerProductRepository = dinnerProductRepository;
        _dailyForDayRepository = dailyForDayRepository;
        _breakfastProductRepository = breakfastProductRepository;
    }

    public async Task Seed(bool recreateDb = false)
    {
        if (recreateDb == true)
        {
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();
        }

        Manufacturer nestle = new Manufacturer
        {
            Name = "Nestle"
        };
        nestle = await _manufacturerRepository.Add(nestle);

        Manufacturer mcDonalds = new Manufacturer
        {
            Name = "McDonald`s"
        };
        mcDonalds = await _manufacturerRepository.Add(mcDonalds);

        Manufacturer sandora = new Manufacturer
        {
            Name = "Sandora"
        };
        sandora = await _manufacturerRepository.Add(sandora);

        Manufacturer silpo = new Manufacturer
        {
            Name = "Silpo"
        };
        silpo = await _manufacturerRepository.Add(silpo);

        Category vegetables = new Category
        {
            Name = "Vegetables"
        };
        vegetables = await _categoryRepository.Add(vegetables);

        Category drinks = new Category
        {
            Name = "Drinks"
        };
        drinks = await _categoryRepository.Add(drinks);

        Category bakery = new Category
        {
            Name = "Bakery"
        };
        bakery = await _categoryRepository.Add(bakery);

        Category groceries = new Category
        {
            Name = "Groceries"
        };
        groceries = await _categoryRepository.Add(groceries);

        Category milkProducts = new Category
        {
            Name = "Milk"
        };
        milkProducts = await _categoryRepository.Add(milkProducts);

        Category fruits = new Category
        {
            Name = "Fruits"
        };
        fruits = await _categoryRepository.Add(fruits);

        Product egg = new Product
        {
            Name = "Egg",
            CaloriePer100g = 153,
            ProteinPer100g = 12.7,
            FatPer100g = 11.1,
            CarbohydratePer100g = 0.6,
            CategoryId = milkProducts.Id
        };
        egg = await _productRepository.Add(egg);

        Product candy = new Product
        {
            Name = "Candy",
            CaloriePer100g = 77,
            ProteinPer100g = 0.5,
            FatPer100g = 2.6,
            CarbohydratePer100g = 67,
        };
        candy = await _productRepository.Add(candy);

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
        porridge = await _productRepository.Add(porridge);

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
        tommato = await _productRepository.Add(tommato);

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
        sandwich = await _productRepository.Add(sandwich);

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
        juice = await _productRepository.Add(juice);

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
        cookie = await _productRepository.Add(cookie);

        DailyForDay dailyFoodDairyUser1 = new DailyForDay
        {
            Date = DateTime.UtcNow
        };
        dailyFoodDairyUser1 = await _dailyForDayRepository.Add(dailyFoodDairyUser1);

        DailyForDay dailyFoodDairyUser2 = new DailyForDay
        {
            Date = DateTime.UtcNow
        };
        dailyFoodDairyUser2 = await _dailyForDayRepository.Add(dailyFoodDairyUser2);

        IdentityResult user1 = await _userManager.CreateAsync(new User
        {
            UserName = "Stephanie",
            Email = "stephanie@gmail.com",
            CurrentWeight = 70,
            DesiredWeight = 60,
            Height = 170,
            Age = 30,
            Gender = Gender.Female,
            ActivityLevel = ActivityLevel.Moderate,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        });

        IdentityResult user2 = await _userManager.CreateAsync(new User
        {
            UserName = "Ivan",
            Email = "ivan@gmail.com",
            CurrentWeight = 77,
            DesiredWeight = 83,
            Height = 180,
            Age = 25,
            Gender = Gender.Male,
            ActivityLevel = ActivityLevel.High,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        });

        BreakfastProduct breakfastProduct1 = new BreakfastProduct
        {
            ProductId = egg.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        breakfastProduct1 = await _breakfastProductRepository.Add(breakfastProduct1);

        BreakfastProduct breakfastProduct2 = new BreakfastProduct
        {
            ProductId = porridge.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        breakfastProduct2 = await _breakfastProductRepository.Add(breakfastProduct2);

        LunchProduct lunchProduct1 = new LunchProduct
        {
            ProductId = sandwich.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        lunchProduct1 = await _lunchProductRepository.Add(lunchProduct1);

        LunchProduct lunchProduct2 = new LunchProduct
        {
            ProductId = tommato.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        lunchProduct2 = await _lunchProductRepository.Add(lunchProduct2);

        DinnerProduct dinnerProduct1 = new DinnerProduct
        {
            ProductId = cookie.Id,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        dinnerProduct1 = await _dinnerProductRepository.Add(dinnerProduct1);

        DinnerProduct dinnerProduct2 = new DinnerProduct
        {
            ProductId = juice.Id,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        dinnerProduct2 = await _dinnerProductRepository.Add(dinnerProduct2);
    }
}
