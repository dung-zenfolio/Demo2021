using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Configurations
{
    public class ProductImagesEntityConfiguration : IEntityTypeConfiguration<ProductImagesEntity>
    {
        public void Configure(EntityTypeBuilder<ProductImagesEntity> builder)
        {
            builder.Property(p => p.ImageName).HasMaxLength(50);
            builder.Property(p => p.ImageUrl).HasMaxLength(500);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
