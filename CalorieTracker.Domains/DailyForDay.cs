namespace CalorieTracker.Domains;

public class DailyForDay : BaseNamedEntity
{
    public List<MealProduct> MealProducts { get; set; } = new List<MealProduct>();

    public  DateTime Date { get; set; }

    public int? UserId { get; set; }

    public User User { get; set; }
}
