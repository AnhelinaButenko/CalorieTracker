using AutoMapper;
using CalorieTracker.Data;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service.Models;

namespace CalorieTracker.Service;

public interface IDailyForDayService
{
    Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date);
}

public class DailyForDayService : IDailyForDayService
{
    private readonly IDailyForDayRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    
    public DailyForDayService(IDailyForDayRepository repository, IMapper mapper, IUserService userService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService)); 
    }

    public async Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date)
    {
        if (userId <= 0)
        {
           throw new ArgumentException("User Id should be greatest than zero!");
        }

        User user = await _userService.GetUserById(userId);

        if (user == null)
        {
            throw new ArgumentException($"User with Id {userId} not found");
        }

        // get DayilyForday by id - > await _repository.GetById(id);

        //1.  var dailyforDay = await _repository.GetDailyForDayForUser(int userID, DateTime date) -> DbContext.DailyFoodDairies
        // .Where(x => userId , date)
        // .Include(x =>)
        // .ThenIclude( =>)

        //calculation logic

        //2.  dailyforDay.BreakfastProducts[0].Product.CaloriePer100g
        // dailyforDay.BreakfastProducts[0].ProductWeightGr

        //3. var result = new DailyForDayUserDto();
        // result.UserId = userId;
        // .... calculation
        // result.CaloriesConsumed = <value>;
        // ...
        // result.

        // return result; //

        return new DailyForDayUserDto()
        {
            UserId = userId,
            CaloriesConsumed = 1200,
            CaloriesLeft = 370,
            CarbohydratesConsumed = 32,
            ProteinsConsumed = 41,
            FatsConsumed = 23,
            DailyMeals = new List<DailyMeal>
            {
                new DailyMeal()
                {
                    MealName = "Breakfast",
                    ProductConsumptions = new List<ProductConsumption>
                    {
                        new ProductConsumption { ProductId = 1, ProductName = "Eggs", CaloriesConsumed = 300, GramsConsumed = 50 },
                        new ProductConsumption { ProductId = 2, ProductName = "Bread", CaloriesConsumed = 200, GramsConsumed = 100 }
                    }
                },
                new DailyMeal()
                {
                    MealName = "Lunch",
                    ProductConsumptions = new List<ProductConsumption>
                    {
                        new ProductConsumption { ProductId = 3, ProductName = "Chicken", CaloriesConsumed = 400, GramsConsumed = 200 },
                        new ProductConsumption { ProductId = 4, ProductName = "Rice", CaloriesConsumed = 300, GramsConsumed = 150 }
                    }
                },
                new DailyMeal()
                {
                    MealName = "Dinner",
                    ProductConsumptions = new List<ProductConsumption>
                    {
                        new ProductConsumption { ProductId = 4, ProductName = "Avocado", CaloriesConsumed = 500, GramsConsumed = 90 },
                        new ProductConsumption { ProductId = 5, ProductName = "Tost", CaloriesConsumed = 200, GramsConsumed = 120 }
                    }
                }
            }
        };
    }
}
