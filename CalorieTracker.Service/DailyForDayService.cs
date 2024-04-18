using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Service;

public interface IDailyForDayService
{
    Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date);
}

public class DailyForDayService : IDailyForDayService
{
    private readonly IDailyForDayRepository _repository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public DailyForDayService(IDailyForDayRepository repository, IUserService userService, IProductService productService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date)
    {
        User user = await _userService.GetUserById(userId);

        if (user == null || userId <= 0)
        {
            throw new ArgumentException(user == null ? $"User with Id {userId} not found" : "User Id should be greater than zero!");
        }

        var dailyForDay = await _repository.GetDailyForDayForUser(userId, date);

        if (dailyForDay == null)
        {
            throw new ArgumentException($"DailyForDay not found for user Id {userId} and date {date}");
        }

        var dailyMeals = new List<DailyMeal>
        {
            new DailyMeal { MealName = "Breakfast", ProductConsumptions = await GetProductConsumptions(dailyForDay.BreakfastProducts) },
            new DailyMeal { MealName = "Lunch", ProductConsumptions = await GetProductConsumptions(dailyForDay.LunchProducts) },
            new DailyMeal { MealName = "Dinner", ProductConsumptions = await GetProductConsumptions(dailyForDay.DinnerProducts) }
        };

        double totalCaloriesConsumed = dailyMeals.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.CaloriesConsumed));
        double totalProteinsConsumed = dailyMeals.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.ProteinsConsumed));
        double totalFatsConsumed = dailyMeals.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.FatsConsumed));
        double totalCarbohydratesConsumed = dailyMeals.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.CarbohydratesConsumed));

        var dailyForDayUserDto = new DailyForDayUserDto
        {
            UserId = userId,
            CaloriesConsumed = (int)totalCaloriesConsumed,
            ProteinsConsumed = totalProteinsConsumed,
            FatsConsumed = totalFatsConsumed,
            CarbohydratesConsumed = totalCarbohydratesConsumed,
            CaloriesLeft = (int)(user.RecommendedCalories - (int)totalCaloriesConsumed),
            DailyMeals = dailyMeals
        };

        return dailyForDayUserDto;
    }

    private async Task<List<ProductConsumption>> GetProductConsumptions(IEnumerable<BreakfastProduct> mealProducts)
    {
        var productConsumptions = new List<ProductConsumption>();

        foreach (var mealProduct in mealProducts)
        {
            var product = mealProduct.Product;
            double calories = (product.CaloriePer100g * mealProduct.GramsConsumed) / 100;
            double proteins = (product.ProteinPer100g * mealProduct.GramsConsumed) / 100;
            double fats = (product.FatPer100g * mealProduct.GramsConsumed) / 100;
            double carbohydrates = (product.CarbohydratePer100g * mealProduct.GramsConsumed) / 100;

            productConsumptions.Add(new ProductConsumption
            {
                ProductId = mealProduct.ProductId,
                ProductName = mealProduct.Product.Name,
                CaloriesConsumed = (int)calories,
                GramsConsumed = mealProduct.GramsConsumed,
                ProteinsConsumed = proteins,
                FatsConsumed = fats,
                CarbohydratesConsumed = carbohydrates
            });
        }
        return productConsumptions;
    }

    private async Task<List<ProductConsumption>> GetProductConsumptions(IEnumerable<LunchProduct> mealProducts)
    {
        var productConsumptions = new List<ProductConsumption>();

        foreach (var mealProduct in mealProducts)
        {
            var product = mealProduct.Product;
            double calories = (product.CaloriePer100g * mealProduct.GramsConsumed) / 100;
            double proteins = (product.ProteinPer100g * mealProduct.GramsConsumed) / 100;
            double fats = (product.FatPer100g * mealProduct.GramsConsumed) / 100;
            double carbohydrates = (product.CarbohydratePer100g * mealProduct.GramsConsumed) / 100;

            productConsumptions.Add(new ProductConsumption
            {
                ProductId = mealProduct.ProductId,
                ProductName = mealProduct.Product.Name,
                CaloriesConsumed = (int)calories,
                GramsConsumed = mealProduct.GramsConsumed,
                ProteinsConsumed = proteins,
                FatsConsumed = fats,
                CarbohydratesConsumed = carbohydrates
            });
        }
        return productConsumptions;
    }

    private async Task<List<ProductConsumption>> GetProductConsumptions(IEnumerable<DinnerProduct> mealProducts)
    {
        var productConsumptions = new List<ProductConsumption>();

        foreach (var mealProduct in mealProducts)
        {
            var product = mealProduct.Product;
            double calories = (product.CaloriePer100g * mealProduct.GramsConsumed) / 100;
            double proteins = (product.ProteinPer100g * mealProduct.GramsConsumed) / 100;
            double fats = (product.FatPer100g * mealProduct.GramsConsumed) / 100;
            double carbohydrates = (product.CarbohydratePer100g * mealProduct.GramsConsumed) / 100;

            productConsumptions.Add(new ProductConsumption
            {
                ProductId = mealProduct.ProductId,
                ProductName = mealProduct.Product.Name,
                CaloriesConsumed = (int)calories,
                GramsConsumed = mealProduct.GramsConsumed,
                ProteinsConsumed = proteins,
                FatsConsumed = fats,
                CarbohydratesConsumed = carbohydrates
            });
        }
        return productConsumptions;
    }
}

//// get DayilyForday by id - > await _repository.GetById(id);

////1.  var dailyforDay = await _repository.GetDailyForDayForUser(int userID, DateTime date) -> DbContext.DailyFoodDairies
//// .Where(x => userId , date)
//// .Include(x =>)
//// .ThenIclude( =>)

////calculation logic

////2.  dailyforDay.BreakfastProducts[0].Product.CaloriePer100g
//// dailyforDay.BreakfastProducts[0].ProductWeightGr

////3. var result = new DailyForDayUserDto();
//// result.UserId = userId;
//// .... calculation
//// result.CaloriesConsumed = <value>;
//// ...
//// result.

//// return result; //