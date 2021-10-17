using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Configurations
{
    public class InvoiceDetailsEntityConfiguration : IEntityTypeConfiguration<InvoiceDetailEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetailEntity> builder)
        {
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.InvoiceId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.ProductQuantity).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.TotalAmount).IsRequired();

            builder.HasOne(p => p.Invoice)
                .WithMany(p => p.InvoiceDetails)
                .HasForeignKey(p => p.InvoiceId);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.InvoiceDetails)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
