using Microsoft.AspNet.Identity.EntityFramework;

namespace CalorieTracker.Domains;

public class User : BaseNamedEntity, IdentityUser
{
    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public int RecommendedCalory { get; set; }

    public DailyFoodDairy DailyFoodDairy { get; set; }
}
