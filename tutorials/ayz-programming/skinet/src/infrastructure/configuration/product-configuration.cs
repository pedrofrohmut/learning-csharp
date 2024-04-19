using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skinet.Core.Entities;

namespace Skinet.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(180);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.PictureUrl).IsRequired();
        builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x => x.ProductBrandId);
        builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.ProductTypeId);
    }
}
