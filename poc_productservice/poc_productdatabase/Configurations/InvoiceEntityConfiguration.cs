using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using poc_productdatabase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace poc_productdatabase.Configurations
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
    {
        public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
        {
            builder.Property(p => p.InvoiceNo).HasMaxLength(20).IsRequired();

            builder.HasMany(p => p.InvoiceDetails)
                .WithOne(p => p.Invoice)
                .HasForeignKey(p => p.InvoiceId);
        }
    }
}
