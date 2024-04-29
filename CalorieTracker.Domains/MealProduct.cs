using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalorieTracker.Domains;

public class MealProduct : BaseNamedEntity
{
    public int ProductId { get; set; }

    public Product Product { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }

    public int GramsConsumed { get; set; }
}
