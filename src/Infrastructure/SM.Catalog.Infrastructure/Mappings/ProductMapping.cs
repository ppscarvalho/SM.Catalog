using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Catalog.Core.Domain.Entities;

namespace SM.Catalog.Infrastructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(c => c.SupplierId)
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.PurchaseValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.SaleValue)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.ProfitMargin)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(c => c.Stock)
                .IsRequired();

            builder.Property(c => c.Status)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
