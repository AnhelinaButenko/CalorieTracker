namespace CalorieTracker.Domains;

public class MealNames
{
    public const string Breakfast = "Breakfast";
    public const string Lunch = "Lunch";
    public const string Dinner = "Dinner";
}

public class MealProduct : BaseEntity
{
    public int ProductId { get; set; }

    public string MealName { get; set; }

    public string? ProductName { get; set; }

    public Product Product { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }

    public int GramsConsumed { get; set; }
}
