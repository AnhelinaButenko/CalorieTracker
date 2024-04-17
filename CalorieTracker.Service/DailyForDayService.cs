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
        if (userId <= 0)
        {
            throw new ArgumentException("User Id should be greater than zero!");
        }

        User user = await _userService.GetUserById(userId);

        if (user == null)
        {
            throw new ArgumentException($"User with Id {userId} not found");
        }

        var dailyForDay = await _repository.GetDailyForDayForUser(userId, date);

        if (dailyForDay == null)
        {
            throw new ArgumentException($"DailyForDay not found for user Id {userId} and date {date}");
        }

        var mealTypes = new Dictionary<string, Func<IEnumerable<ProductConsumption>>>
{
    { "Breakfast", () => dailyForDay.BreakfastProducts.Select(bp => new ProductConsumption
        {
            ProductId = bp.ProductId,
            ProductName = bp.Product.Name,
            CaloriesConsumed = (int)((bp.Product.CaloriePer100g * bp.GramsConsumed) / 100.0),
            GramsConsumed = bp.GramsConsumed,
            ProteinsConsumed = (bp.Product.ProteinPer100g * bp.GramsConsumed) / 100.0,
            FatsConsumed = (bp.Product.FatPer100g * bp.GramsConsumed) / 100.0,
            CarbohydratesConsumed = (bp.Product.CarbohydratePer100g * bp.GramsConsumed) / 100.0
        }).ToList() }, 
    { "Lunch", () => dailyForDay.LunchProducts.Select(lp => new ProductConsumption
        {
            ProductId = lp.ProductId,
            ProductName = lp.Product.Name,
            CaloriesConsumed = (int)((lp.Product.CaloriePer100g * lp.GramsConsumed) / 100.0),
            GramsConsumed = lp.GramsConsumed,
            ProteinsConsumed = (lp.Product.ProteinPer100g * lp.GramsConsumed) / 100.0,
            FatsConsumed = (lp.Product.FatPer100g * lp.GramsConsumed) / 100.0,
            CarbohydratesConsumed = (lp.Product.CarbohydratePer100g * lp.GramsConsumed) / 100.0
        }).ToList() }, 
    { "Dinner", () => dailyForDay.DinnerProducts.Select(dp => new ProductConsumption
        {
            ProductId = dp.ProductId,
            ProductName = dp.Product.Name,
            CaloriesConsumed = (int)((dp.Product.CaloriePer100g * dp.GramsConsumed) / 100.0),
            GramsConsumed = dp.GramsConsumed,
            ProteinsConsumed = (dp.Product.ProteinPer100g * dp.GramsConsumed) / 100.0,
            FatsConsumed = (dp.Product.FatPer100g * dp.GramsConsumed) / 100.0,
            CarbohydratesConsumed = (dp.Product.CarbohydratePer100g * dp.GramsConsumed) / 100.0
        }).ToList() } 
};

        var result = new DailyForDayUserDto
        {
            UserId = userId,
            DailyMeals = new List<DailyMeal>()
        };

        foreach (var mealType in mealTypes.Keys)
        {
            var meal = new DailyMeal { MealName = mealType, ProductConsumptions = new List<ProductConsumption>() };

            var products = mealTypes[mealType]();

            foreach (var product in products)
            {
                var productDto = await _productService.GetProductById(product.ProductId);

                if (productDto == null)
                {
                    throw new ArgumentException($"Product not found for Id {product.ProductId}");
                }

                var caloriesPer100g = productDto.CaloriePer100g;
                var gramsConsumed = product.GramsConsumed;
                var consumed = (caloriesPer100g * gramsConsumed) / 100.0;

                var productConsumption = new ProductConsumption
                {
                    ProductId = product.ProductId,
                    ProductName = productDto.Name,
                    CaloriesConsumed = (int)consumed,
                    GramsConsumed = gramsConsumed,
                    ProteinsConsumed = (productDto.ProteinPer100g * gramsConsumed) / 100.0,
                    FatsConsumed = (productDto.FatPer100g * gramsConsumed) / 100.0,
                    CarbohydratesConsumed = (productDto.CarbohydratePer100g * gramsConsumed) / 100.0
                };

                meal.ProductConsumptions.Add(productConsumption);
            }

            result.DailyMeals.Add(meal);
        }

        result.CaloriesConsumed = result.DailyMeals.Sum(m => m.ProductConsumptions.Sum(pc => pc.CaloriesConsumed));
        result.ProteinsConsumed = result.DailyMeals.Sum(m => m.ProductConsumptions.Sum(pc => pc.ProteinsConsumed));
        result.FatsConsumed = result.DailyMeals.Sum(m => m.ProductConsumptions.Sum(pc => pc.FatsConsumed));
        result.CarbohydratesConsumed = result.DailyMeals.Sum(m => m.ProductConsumptions.Sum(pc => pc.CarbohydratesConsumed));

        result.CaloriesLeft = (int)(user.RecommendedCalories.GetValueOrDefault(2500) - result.CaloriesConsumed);

        return result;
    }

        //public async Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date)
        //{
        //    if (userId <= 0)
        //    {
        //        throw new ArgumentException("User Id should be greater than zero!");
        //    }

        //    User user = await _userService.GetUserById(userId);

        //    if (user == null)
        //    {
        //        throw new ArgumentException($"User with Id {userId} not found");
        //    }

        //    var dailyForDay = await _repository.GetDailyForDayForUser(userId, date);

        //    if (dailyForDay == null)
        //    {
        //        throw new ArgumentException($"DailyForDay not found for user Id {userId} and date {date}");
        //    }

        //    double totalCaloriesConsumed = 0;
        //    double totalProteinsConsumed = 0;
        //    double totalFatsConsumed = 0;
        //    double totalCarbohydratesConsumed = 0;

        //    foreach (var breakfastProduct in dailyForDay.BreakfastProducts)
        //    {
        //        totalCaloriesConsumed += (breakfastProduct.Product.CaloriePer100g * breakfastProduct.ProductWeightGr) / 100;
        //        totalProteinsConsumed += (breakfastProduct.Product.ProteinPer100g * breakfastProduct.ProductWeightGr) / 100;
        //        totalFatsConsumed += (breakfastProduct.Product.FatPer100g * breakfastProduct.ProductWeightGr) / 100;
        //        totalCarbohydratesConsumed += (breakfastProduct.Product.CarbohydratePer100g * breakfastProduct.ProductWeightGr) / 100;
        //    }

        //    var result = new DailyForDayUserDto
        //    {
        //        UserId = userId,
        //        CaloriesConsumed = (int)totalCaloriesConsumed,
        //        CaloriesLeft = 2500 - (int)totalCaloriesConsumed,
        //        ProteinsConsumed = totalProteinsConsumed,
        //        FatsConsumed = totalFatsConsumed,
        //        CarbohydratesConsumed = totalCarbohydratesConsumed,
        //        DailyMeals = dailyForDay.BreakfastProducts.Select(bp => new DailyMeal
        //        {
        //            MealName = "Breakfast",
        //            ProductConsumptions = new List<ProductConsumption>
        //            {
        //                new ProductConsumption
        //                {
        //                    ProductId = bp.Product.Id,
        //                    ProductName = bp.Product.Name,
        //                    CaloriesConsumed = (int)((bp.Product.CaloriePer100g * bp.ProductWeightGr) / 100),
        //                    GramsConsumed = bp.ProductWeightGr
        //                }
        //            }
        //        }).ToList()
        //    };

        //    return result;
        //}
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

//return new DailyForDayUserDto()
//{
//    UserId = userId,
//            CaloriesConsumed = 1200,
//            CaloriesLeft = 370,
//            CarbohydratesConsumed = 32,
//            ProteinsConsumed = 41,
//            FatsConsumed = 23,
//            DailyMeals = new List<DailyMeal>
//            {
//                new DailyMeal()
//                {
//                    MealName = "Breakfast",
//                    ProductConsumptions = new List<ProductConsumption>
//                    {
//                        new ProductConsumption { ProductId = 1, ProductName = "Eggs", CaloriesConsumed = 300, GramsConsumed = 50 },
//                        new ProductConsumption { ProductId = 2, ProductName = "Bread", CaloriesConsumed = 200, GramsConsumed = 100 }
//                    }
//                },
//                new DailyMeal()
//                {
//                    MealName = "Lunch",
//                    ProductConsumptions = new List<ProductConsumption>
//                    {
//                        new ProductConsumption { ProductId = 3, ProductName = "Chicken", CaloriesConsumed = 400, GramsConsumed = 200 },
//                        new ProductConsumption { ProductId = 4, ProductName = "Rice", CaloriesConsumed = 300, GramsConsumed = 150 }
//                    }
//                },
//                new DailyMeal()
//                {
//                    MealName = "Dinner",
//                    ProductConsumptions = new List<ProductConsumption>
//                    {
//                        new ProductConsumption { ProductId = 4, ProductName = "Avocado", CaloriesConsumed = 500, GramsConsumed = 90 },
//                        new ProductConsumption { ProductId = 5, ProductName = "Tost", CaloriesConsumed = 200, GramsConsumed = 120 }
//                    }
//                }
//            }
//        };