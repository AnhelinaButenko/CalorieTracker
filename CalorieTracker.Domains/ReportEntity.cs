﻿namespace CalorieTracker.Domains;

public abstract class ReportEntity : BaseEntity
{
    public double TotalCalories { get; set; }
    
    public double TotalAmountProteins { get; set; }

    public double TotalAmountFats { get; set; }

    public double TotalAmountCarbohydrates { get; set; }
}
