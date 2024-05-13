using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using CalorieTracker.Service.Models;

namespace CalorieTracker.Service;

public interface IDailyForDayService
{
    Task<DailyForDayUserDto> GetDailyForDayDtoForCertainUser(int userId, DateTime date);
    Task<DailyForDayUserDto> RemoveProductFromMealProductForCertainUser(int userId, int mealProductId, DateTime date);
    Task<DailyForDay> EditProductFromMealProductForCertainUser(MealProductDto mealProductDto, int userId, int mealProductId, DateTime date);
    Task<MealProductDto> GetProductFromMealProductWithSummaryForCertainUser(MealProductSummaryRequestDto mealProductSummaryRequestDto);
}

public class DailyForDayService : IDailyForDayService
{
    private readonly IDailyForDayRepository _repository;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    public DailyForDayService(IDailyForDayRepository repository, IUserService userService, IProductService productService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
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

        var dailyMeals = await Task.WhenAll(dailyForDay.MealProducts.GroupBy(m => m.MealName).Select(async group => new DailyMeal
        {
            MealName = group.Key,
            ProductConsumptions = await GetProductConsumptions(group)
        }).ToList());

        var dailyMealsList = dailyMeals.ToList();

        double totalCaloriesConsumed = dailyMealsList.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.CaloriesConsumed));
        double totalProteinsConsumed = dailyMealsList.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.ProteinsConsumed));
        double totalFatsConsumed = dailyMealsList.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.FatsConsumed));
        double totalCarbohydratesConsumed = dailyMealsList.Sum(meal => meal.ProductConsumptions.Sum(pc => pc.CarbohydratesConsumed));

        DailyForDayUserDto dailyForDayUserDto = new DailyForDayUserDto
        {
            UserId = userId,
            CaloriesConsumed = (int)totalCaloriesConsumed,
            ProteinsConsumed = totalProteinsConsumed,
            FatsConsumed = totalFatsConsumed,
            CarbohydratesConsumed = totalCarbohydratesConsumed,
            CaloriesLeft = (int)(user.RecommendedCalories - (int)totalCaloriesConsumed),
            DailyMeals = dailyMealsList,
        };

        return dailyForDayUserDto;
    }

    public async Task<MealProductDto> GetProductFromMealProductWithSummaryForCertainUser(MealProductSummaryRequestDto mealProductSummaryRequestDto)
    {
        DailyForDay? dailyForDay = await _repository.GetDailyForDayForUser(mealProductSummaryRequestDto.UserId, mealProductSummaryRequestDto.DateTime);

        if (dailyForDay == null)
        {
            throw new ArgumentException($"DailyForDay not found for user Id {mealProductSummaryRequestDto?.UserId} and date {mealProductSummaryRequestDto.DateTime}");
        }

        MealProduct? mealProductToUpdate = dailyForDay.MealProducts.FirstOrDefault(p => p.Id == mealProductSummaryRequestDto.MealProductId);

        if (mealProductToUpdate == null)
        {
            throw new ArgumentException($"MealProduct not found with Id {mealProductSummaryRequestDto?.MealProductId}");
        }

        if (mealProductSummaryRequestDto?.MealProductId <= 0)
        {
            throw new ArgumentException($"MealProduct not found with Id {mealProductSummaryRequestDto?.MealProductId}");
        }

        if (mealProductSummaryRequestDto?.UserId <= 0)
        {
            throw new ArgumentException($"UserId not found with Id {mealProductSummaryRequestDto?.UserId}");
        }

        var mealProductDto = new MealProductDto
        {
            MealName = mealProductToUpdate.MealName,
            MealProductId = mealProductToUpdate.Id,
            ProductId = mealProductToUpdate.ProductId,
            ProductName = mealProductToUpdate.Product.Name,
            ProductWeightGr = mealProductToUpdate.GramsConsumed,
            DateTime = mealProductSummaryRequestDto.DateTime,
        };

        return mealProductDto;
    }

    public async Task<DailyForDay> EditProductFromMealProductForCertainUser(MealProductDto mealProductDto, int userId, int mealProductId, DateTime date)
    {
        DailyForDay? dailyForDay = await _repository.GetDailyForDayForUser(userId, date);

        if (dailyForDay == null)
        {
            throw new ArgumentException($"DailyForDay not found for user Id {userId} and date {date}");
        }

        MealProduct? mealProductToUpdate = dailyForDay.MealProducts.FirstOrDefault(p => p.Id == mealProductId);

        if (mealProductToUpdate == null)
        {
            throw new ArgumentException($"MealProduct not found with Id {mealProductId}");
        }

        mealProductToUpdate.GramsConsumed = mealProductDto.ProductWeightGr;

        await _repository.Update(dailyForDay.Id, dailyForDay);

        return dailyForDay;
    }

    public async Task<DailyForDayUserDto> RemoveProductFromMealProductForCertainUser(int userId, int mealProductId, DateTime date)
    {
        DailyForDay? dailyForDay = await _repository.GetDailyForDayForUser(userId, date);

        if (dailyForDay == null)
        {
            throw new ArgumentException($"DailyForday not found for user Id {userId} and date {date}");
        }

        MealProduct? mealProductToDelete = dailyForDay.MealProducts.FirstOrDefault(x => x.Id == mealProductId);

        if (mealProductToDelete == null)
        {
            throw new ArgumentException($"MealProduct not found {mealProductToDelete}");
        }

        dailyForDay.MealProducts.Remove(mealProductToDelete);

        await _repository.Update(dailyForDay.Id, dailyForDay);

        return await GetDailyForDayDtoForCertainUser(userId, date);
    }

    private async Task<List<ProductConsumption>>? GetProductConsumptions(IEnumerable<MealProduct> mealProducts)
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
                CarbohydratesConsumed = carbohydrates,
                MealProductId = mealProduct.Id,
            });
        }
        return productConsumptions;
    }
}