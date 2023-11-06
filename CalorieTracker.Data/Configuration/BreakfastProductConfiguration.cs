﻿using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieTracker.Data.Configuration; 
public class BreakfastProductConfiguration  : IEntityTypeConfiguration<BreakfastProduct>
{
    public void Configure(EntityTypeBuilder<BreakfastProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityProduct);

        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.DailyFoodDairy).WithMany(x => x.BreakfastProducts).HasForeignKey(x => x.DailyFoodDairyId);
    }
}
