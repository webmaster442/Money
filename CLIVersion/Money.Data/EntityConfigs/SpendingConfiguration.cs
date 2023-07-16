using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Money.Data.Entities;

namespace Money.Data.EntityConfigs;

internal sealed class SpendingConfiguration : IEntityTypeConfiguration<Spending>
{
    public void Configure(EntityTypeBuilder<Spending> builder)
    {
        builder.HasKey(spending => spending.Id);
        builder.Property(spending => spending.Ammount).IsRequired();
        builder.Property(spending => spending.Description).IsRequired();
        builder.Property(spending => spending.Date).IsRequired();
        builder.Property(spending => spending.AddedOn).IsRequired();

        builder.HasIndex(spending => spending.Date);
        builder.HasIndex(spending => spending.Description);
    }
}
