using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieTracker.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.CaloriePer100g);

        builder.Property(x => x.ProteinPer100g);

        builder.Property(x => x.FatPer100g);

        builder.Property(x => x.CarbohydratePer100g);
    }
}
