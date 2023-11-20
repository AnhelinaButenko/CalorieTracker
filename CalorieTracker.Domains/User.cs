using Microsoft.AspNetCore.Identity;

namespace CalorieTracker.Domains;

public class User : IdentityUser<int>, IBaseNamedEntity
{
    int IBaseEntity.Id { get { return Id; } set { Id = value; } }

    public string Name { get; set; }

    public double CurrentWeight { get; set; }

    public double DesiredWeight { get; set; }

    public int DailyFoodDairyId { get; set; }

    public DailyForDay DailyFoodDairy { get; set; }
}
