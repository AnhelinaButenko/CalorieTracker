using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieTracker.Data.Configuration;

public class MealProductConfiguration : IEntityTypeConfiguration<MealProduct>
{
    public void Configure(EntityTypeBuilder<MealProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GramsConsumed);

        builder.Property(x => x.MealName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.MealName);
    }
}

