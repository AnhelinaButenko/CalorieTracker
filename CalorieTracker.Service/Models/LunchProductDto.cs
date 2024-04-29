namespace CalorieTracker.Service.Models;

public class LunchProductDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int ProductWeightGr { get; set; }

    public int ProductId { get; set; }

    public int DailyFoodDairyId { get; set; }
}
