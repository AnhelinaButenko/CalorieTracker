namespace CalorieTracker.Api.Models;

public class MealProductDto
{
    public int Id { get; set; }

    public int MealProductId { get; set; }

    public string Name { get; set; }

    public int ProductWeightGr { get; set; }

    public int ProductId { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DateTime DateTime { get; set; }
}