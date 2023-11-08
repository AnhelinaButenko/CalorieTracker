﻿using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace CalorieTracker.Data.Configuration;

public class DinnerProductConfiguration : IEntityTypeConfiguration<DinnerProduct>
{
    public void Configure(EntityTypeBuilder<DinnerProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityProduct);

        builder.Property(x => x.TotalCalories);

        builder.Property(x => x.TotalAmountProteins);

        builder.Property(x => x.TotalAmountFats);

        builder.Property(x => x.TotalAmountCarbohydrates);

        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.DailyFoodDairy)
            .WithMany(x => x.DinnerProducts)
            .HasForeignKey(x => x.DailyFoodDairyId);
    }
}