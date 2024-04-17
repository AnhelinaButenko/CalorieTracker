using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalorieTracker.Data.Configuration; 
public class BreakfastProductConfiguration  : IEntityTypeConfiguration<BreakfastProduct>
{
    public void Configure(EntityTypeBuilder<BreakfastProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.GramsConsumed);
    }
}
