using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Configurations
{
    public class ProductTypeEntityConfiguration : IEntityTypeConfiguration<ProductTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ProductTypeEntity> builder)
        {
            builder.Property(p => p.TypeName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Rank).IsRequired();

            builder.HasMany(p => p.Products)
                .WithOne(p => p.ProductType)
                .HasForeignKey(p => p.ProductTypeId);
        }
    }
}
