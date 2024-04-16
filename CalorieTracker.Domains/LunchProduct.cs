﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CalorieTracker.Domains;

public class LunchProduct : BaseNamedEntity
{
    public int ProductWeightGr { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }

    public int GramsConsumed { get; set; }
}
