using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace CalorieTracker.Data.Configuration;

public class DailyForDayConfiguration : IEntityTypeConfiguration<DailyForDay>
{
    public void Configure(EntityTypeBuilder<DailyForDay> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MealProducts);

        builder.HasMany(x => x.MealProducts)
            .WithOne(x => x.DailyFoodDairy)
            .HasForeignKey(x => x.DailyFoodDairyId);
    }
}