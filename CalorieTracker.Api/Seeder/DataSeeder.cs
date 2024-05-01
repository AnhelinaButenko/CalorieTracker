using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Domains.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace CalorieTracker.Api.Seeder;

public class DataSeeder : IDataSeeder
{
    private readonly CalorieTrackerDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMealProductRepository _mealProductRepository;
    private readonly IDailyForDayRepository _dailyForDayRepository;

    public DataSeeder(CalorieTrackerDbContext dbContext, UserManager<User> userManager,
         IManufacturerRepository manufacturerRepository, IProductRepository productRepository, 
         ICategoryRepository categoryRepository, IMealProductRepository mealProductRepository,
         IDailyForDayRepository dailyForDayRepository)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _manufacturerRepository = manufacturerRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _dailyForDayRepository = dailyForDayRepository;
        _mealProductRepository = mealProductRepository;
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

        User user1 = new User
        {
            UserName = "Stephanie",
            Email = "stephanie@gmail.com",
            CurrentWeight = 70,
            DesiredWeight = 60,
            Height = 170,
            Age = 30,
            Gender = Gender.Female,
            ActivityLevel = ActivityLevel.Moderate,
            RecommendedCalories = 2100,
            RecommendedCarbs = 60,
            RecommendedFat = 90,
            RecommendedProtein = 60,
        };
        IdentityResult user1Result = await _userManager.CreateAsync(user1);

        User user2 = new User
        {
            UserName = "Ivan",
            Email = "ivan@gmail.com",
            CurrentWeight = 77,
            DesiredWeight = 83,
            Height = 180,
            Age = 25,
            Gender = Gender.Male,
            ActivityLevel = ActivityLevel.High,
            RecommendedCalories = 2700,
            RecommendedCarbs = 100,
            RecommendedFat = 96,
            RecommendedProtein = 82,
        };
        IdentityResult user2Result = await _userManager.CreateAsync(user2);

        DailyForDay dailyFoodDairyUser1 = new DailyForDay
        {
            UserId = user1?.Id,
            Date = new DateTime(2024, 04, 13)
        };
        dailyFoodDairyUser1 = await _dailyForDayRepository.Add(dailyFoodDairyUser1);

        DailyForDay dailyFoodDairyUser2 = new DailyForDay
        {
            UserId = user2.Id,
            Date = new DateTime(2024, 04, 13)
        };
        dailyFoodDairyUser2 = await _dailyForDayRepository.Add(dailyFoodDairyUser2);

        MealProduct breakfastProduct1 = new MealProduct
        {
            MealName = MealNames.Breakfast,
            ProductId = egg.Id,
            GramsConsumed = 93,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        breakfastProduct1 = await _mealProductRepository.Add(breakfastProduct1);

        MealProduct breakfastProduct3 = new MealProduct
        {
            MealName= MealNames.Breakfast,
            ProductId = candy.Id,
            GramsConsumed = 93,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        breakfastProduct3 = await _mealProductRepository.Add(breakfastProduct3);

        MealProduct breakfastProduct2 = new MealProduct
        {
            MealName = MealNames.Breakfast,
            ProductId = porridge.Id,
            GramsConsumed = 202,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        breakfastProduct2 = await _mealProductRepository.Add(breakfastProduct2);

        MealProduct lunchProduct1 = new MealProduct
        {
            MealName = MealNames.Lunch,
            ProductId = egg.Id,
            GramsConsumed = 110,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        lunchProduct1 = await _mealProductRepository.Add(lunchProduct1);

        MealProduct lunchProduct3 = new MealProduct
        {
            MealName = MealNames.Lunch,
            ProductId = sandwich.Id,
            GramsConsumed = 300,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        lunchProduct3 = await _mealProductRepository.Add(lunchProduct3);

        MealProduct lunchProduct2 = new MealProduct
        {
            MealName = MealNames.Lunch,
            ProductId = tommato.Id,
            GramsConsumed = 224,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        lunchProduct2 = await _mealProductRepository.Add(lunchProduct2);

        MealProduct dinnerProduct1 = new MealProduct
        {
            MealName = MealNames.Dinner,
            ProductId = cookie.Id,
            GramsConsumed = 60,
            DailyFoodDairyId = dailyFoodDairyUser2.Id
        };
        dinnerProduct1 = await _mealProductRepository.Add(dinnerProduct1);

        MealProduct dinnerProduct2 = new MealProduct
        {
            MealName = MealNames.Dinner,
            ProductId = juice.Id,
            GramsConsumed = 300,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        dinnerProduct2 = await _mealProductRepository.Add(dinnerProduct2);

        MealProduct dinnerProduct3 = new MealProduct
        {
            MealName = MealNames.Dinner,
            ProductId = cookie.Id,
            GramsConsumed = 300,
            DailyFoodDairyId = dailyFoodDairyUser1.Id
        };
        dinnerProduct3 = await _mealProductRepository.Add(dinnerProduct3);
    }
}
