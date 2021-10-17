using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc_productdatabase.Entities;

namespace poc_productdatabase.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(p => p.ProductName).HasMaxLength(200).IsRequired();
            builder.Property(p => p.ProductQuantity).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.ProductTypeId).IsRequired();

            builder.HasOne(p => p.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProductTypeId);

            builder.HasMany(p => p.InvoiceDetails)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Images)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
