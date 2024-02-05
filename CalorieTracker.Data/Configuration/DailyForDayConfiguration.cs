using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace CalorieTracker.Data.Configuration;

public class DailyForDayConfiguration : IEntityTypeConfiguration<DailyForDay>
{
    public void Configure(EntityTypeBuilder<DailyForDay> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BreakfastProducts);

        builder.Property(x => x.LunchProducts);

        builder.Property(x => x.DinnerProducts);

        builder.HasOne(x => x.User)
        .WithOne(x => x.DailyFoodDairy)
        .HasForeignKey<User>(x => x.DailyFoodDairyId);

        builder.HasMany(x => x.BreakfastProducts)
       .WithOne(x => x.DailyFoodDairy)
       .HasForeignKey(x => x.DailyFoodDairyId);

        builder.HasMany(x => x.LunchProducts)
        .WithOne(x => x.DailyFoodDairy)
        .HasForeignKey(x => x.DailyFoodDairyId);

        builder.HasMany(x => x.DinnerProducts)
        .WithOne(x => x.DailyFoodDairy)
        .HasForeignKey(x => x.DailyFoodDairyId);
    }
}