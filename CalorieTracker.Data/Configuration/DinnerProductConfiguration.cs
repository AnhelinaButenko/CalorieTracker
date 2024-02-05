using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace CalorieTracker.Data.Configuration;

public class DinnerProductConfiguration : IEntityTypeConfiguration<DinnerProduct>
{
    public void Configure(EntityTypeBuilder<DinnerProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductWeightGr);
    }
}
