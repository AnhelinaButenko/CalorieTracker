using CalorieTracker.Domains.Enums;
using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Domains;

public class User : IdentityUser<int>, IBaseEntity
{
    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public double Height { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public ActivityLevel ActivityLevel { get; set; }

    //public int DailyFoodDairyId { get; set; }

    //public DailyForDay DailyFoodDairy { get; set; }

    public List<DailyForDay> DailyFoodDiaries { get; set; } = new List<DailyForDay>();

    public double? RecommendedCalories { get; set; }

    public double? RecommendedProtein { get; set; }

    public double? RecommendedFat { get; set; }

    public double? RecommendedCarbs { get; set; }
}
