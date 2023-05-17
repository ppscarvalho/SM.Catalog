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

    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");

            builder.Property(c => c.Id)
                .IsRequired();

            builder.Property(c => c.CorporateName)
                .HasMaxLength(100)
                .IsRequired();

            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Product)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);
        }
    }
}
