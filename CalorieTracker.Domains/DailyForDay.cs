namespace CalorieTracker.Domains;

public class DailyForDay : BaseNamedEntity
{
    public List<MealProduct> BreakfastProducts { get; set; } = new List<MealProduct>();

    public List<MealProduct> LunchProducts { get; set; } = new List<MealProduct>();

    public List<MealProduct> DinnerProducts { get; set; } = new List<MealProduct>();

    public  DateTime Date { get; set; }

    public int? UserId { get; set; }

    public User User { get; set; }
}
