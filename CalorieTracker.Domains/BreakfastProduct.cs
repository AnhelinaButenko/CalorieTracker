namespace CalorieTracker.Domains;

public class BreakfastProduct : ReportEntity
{
    public int QuantityProduct { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyFoodDairy DailyFoodDairy { get; set; }
}
