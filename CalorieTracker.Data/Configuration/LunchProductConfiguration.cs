using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CalorieTracker.Data.Configuration;

public class LunchProductConfiguration : IEntityTypeConfiguration<LunchProduct>
{
    public void Configure(EntityTypeBuilder<LunchProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductWeightGr);
    }
}