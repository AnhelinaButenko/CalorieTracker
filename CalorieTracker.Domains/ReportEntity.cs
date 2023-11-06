namespace CalorieTracker.Domains;

public abstract class ReportEntity : BaseEntity
{
    public int TotalCalories { get; set; }

    public int TotalAmountProteins { get; set; }

    public int TotalAmountFats { get; set; }

    public int TotalAmountCarbohydrates { get; set; }
}
