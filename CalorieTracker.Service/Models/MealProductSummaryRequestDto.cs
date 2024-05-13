namespace CalorieTracker.Service.Models;

public class MealProductSummaryRequestDto
{
    public int MealProductId { get; set; }

    public int UserId { get; set; }

    public DateTime DateTime { get; set; }
}
