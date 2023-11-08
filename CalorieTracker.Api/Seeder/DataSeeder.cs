using CalorieTracker.Data;
using CalorieTracker.Domains;

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
        bool isCreated = await _dbContext.Database.EnsureCreatedAsync();

        //await _dbContext.Database.EnsureDeletedAsync();

        if (!isCreated)
        {
            return;
        }

        User user1 = new User
        {
            Name = "Lina",
            CurrentWeight = 59,
            DesiredWeight = 57,
            RecommendedCalory = 1500
        };
        await _dbContext.AddAsync(user1);

        User user2 = new User
        {
            Name = "Andrew",
            CurrentWeight = 78,
            DesiredWeight = 82,
            RecommendedCalory = 3000,            
        };
        await _dbContext.AddAsync(user2);

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

        Product porridge = new Product
        {
            Name = "Porridge",
            CaloriePer100g = 137,
            ProteinPer100g = 4.5,
            FatPer100g = 1.6,
            CarbohydratePer100g = 27.4
        };
        await _dbContext.AddAsync(porridge);

        Product tommato = new Product
        {
            Name = "Tommato",
            CaloriePer100g = 18,
            ProteinPer100g = 0.5,
            FatPer100g = 1.1,
            CarbohydratePer100g = 34.2
        };
        await _dbContext.AddAsync(tommato);

        Product juice = new Product
        {
            Name = "Juice",
            CaloriePer100g = 39,
            ProteinPer100g = 0.9,
            FatPer100g = 0.2,
            CarbohydratePer100g = 9.2
        };
        await _dbContext.AddAsync(juice);

        Product cookie = new Product
        {
            Name = "Cookie",
            CaloriePer100g = 93,
            ProteinPer100g = 1.4,
            FatPer100g = 3.2,
            CarbohydratePer100g = 119.1
        };
        await _dbContext.AddAsync(cookie);

        BreakfastProduct scrumbledegg = new BreakfastProduct
        {          
            ProductId = egg.Id,
            QuantityProduct = 1,
            TotalCalories = 90,
            TotalAmountCarbohydrates = 12.3,
            TotalAmountFats = 10.2,
            TotalAmountProteins = 15.5     
        };
        await _dbContext.AddAsync(scrumbledegg);

        LunchProduct porridgeWithTommato = new LunchProduct
        {
            ProductId = porridge.Id,
            QuantityProduct = 2,
            TotalCalories = 203,
            TotalAmountCarbohydrates = 112.3,
            TotalAmountFats = 6.7,
            TotalAmountProteins = 2.50
        };

        porridgeWithTommato = new LunchProduct
        {
            ProductId = tommato.Id,
            QuantityProduct = 2,
            TotalCalories = 203,
            TotalAmountCarbohydrates = 112.3,
            TotalAmountFats = 6.7,
            TotalAmountProteins = 2.5
        };
        await _dbContext.AddAsync(porridgeWithTommato);
        
        DinnerProduct juiceWithCookie = new DinnerProduct
        {

            QuantityProduct = 3,
            TotalCalories = 150,
            TotalAmountCarbohydrates = 214.1,
            TotalAmountFats = 5,
            TotalAmountProteins = 3.4
        };
        await _dbContext.AddAsync(juiceWithCookie);

        DailyFoodDairy dailyFoodDairy = new DailyFoodDairy
        {
            UserId = 1,
            BreakfastProducts = new List<BreakfastProduct>
            {
                scrumbledegg
            },
            LunchProducts = new List<LunchProduct>
            {
                porridgeWithTommato
            },
            DinnerProducts = new List<DinnerProduct>
            { 
                juiceWithCookie
            },

            TotalCalories = 1750,
            TotalAmountCarbohydrates = 214.1,
            TotalAmountFats = 34,
            TotalAmountProteins = 107.4
        };
        await _dbContext.AddAsync(dailyFoodDairy);



        await _dbContext.SaveChangesAsync();
    }
}
