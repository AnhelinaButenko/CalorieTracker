namespace CalorieTracker.Api.Models;

public class DailyForDayUserDto
{
    public int UserId { get; set; }

    public int CaloriesLeft { get; set; }

    public int CaloriesConsumed { get; set;}

    public List<DailyMeal> DailyMeals { get; set; } = new List<DailyMeal>();

    public double ProteinsConsumed { get; set; }

    public double FatsConsumed { get; set; }

    public double CarbohydratesConsumed { get; set; }
}

public class DailyMeal
{
    public string MealName { get; set; }

    public List<ProductConsumption> ProductConsumptions { get; set; }
}

public class ProductConsumption
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public int CaloriesConsumed { get; set; }

    public int GramsConsumed { get; set; }

    public double ProteinsConsumed { get; set; }

    public double FatsConsumed { get; set; }

    public double CarbohydratesConsumed { get; set; }
}