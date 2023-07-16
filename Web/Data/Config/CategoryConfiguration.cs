using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money.Web.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace Money.Web.Data.Config
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(category  => category.Id);
            builder.Property(category => category.Name).IsRequired();
            builder.HasIndex(category => category.Name).IsUnique();

            builder.Property(category => category.Description);
        }
    }
}
