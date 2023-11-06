using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Configuration;

public class DailyForDayConfiguration : IEntityTypeConfiguration<DailyFoodDairy>
{
    public void Configure(EntityTypeBuilder<DailyFoodDairy> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalCalories);

        builder.Property(x => x.TotalAmountProteins);

        builder.Property(x => x.TotalAmountFats);

        builder.Property(x => x.TotalAmountCarbohydrates);

        builder.HasOne(x => x.User)
            .WithOne(x => x.DailyFoodDairy)
            .HasForeignKey<User>(x => x.Id);
    }
}