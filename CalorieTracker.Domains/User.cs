using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Domains;

public enum ActivityLevel
{
    Minimal = 1,
    Low,
    Moderate,
    High,   
    VeryHigh 
}

public enum Gender
{
    Male = 1,
    Female
}


public class User : IdentityUser<int>, IBaseEntity
{
    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public double Height { get; set; }

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public ActivityLevel ActivityLevel { get; set; }

    private static readonly Dictionary<ActivityLevel, double> ActivityCoefficients = new Dictionary<ActivityLevel, double>
    {
        { ActivityLevel.Minimal, 1.2 },
        { ActivityLevel.Low, 1.375 },
        { ActivityLevel.Moderate, 1.55 },
        { ActivityLevel.High, 1.725 },
        { ActivityLevel.VeryHigh, 1.9 }
    };

    public double GetActivityCoefficient()
    {
        if (ActivityCoefficients.TryGetValue(ActivityLevel, out double coefficient))
        {
            return coefficient;
        }
        else
        {
            throw new ArgumentException("Invalid activity level");
        }
    }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }
}
