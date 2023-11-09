using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.CurrentWeight);

        builder.Property(x => x.DesiredWeight);

        builder.Property(x => x.RecommendedCalory);

        builder.Property(x => x.DailyFoodDairy);
    }
}
