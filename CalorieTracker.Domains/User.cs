using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Domains;

public class User : IdentityUser<int>, IBaseNamedEntity
{
    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public double RecommendedCalory { get; set; }

    public DailyFoodDairy DailyFoodDairy { get; set; }

    public string Name { get ; set; }

    int IBaseEntity.Id { get { return Id; } set { Id = value; } }
}
