using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Domains;

public class User : IdentityUser<int>, IBaseEntity
{
    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }
}
