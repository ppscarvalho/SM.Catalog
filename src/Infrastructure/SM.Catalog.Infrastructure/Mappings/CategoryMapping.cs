using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Catalog.Core.Domain.Entities;

namespace SM.Catalog.Infrastructure.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.Property(c => c.Description).HasMaxLength(250)
                .IsRequired();

            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Product)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }

    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.Property(c => c.Name)
                .HasMaxLength(100)
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

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.Status)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
