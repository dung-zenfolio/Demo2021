using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using poc_productdatabase.Configurations;
using poc_productdatabase.Entities;
using System;

namespace poc_productdatabase.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<ProductTypeEntity> ProductType { get; set; }
        public DbSet<ProductImagesEntity> ProductImages { get; set; }
        public DbSet<InvoiceEntity> Invoice { get; set; }
        public DbSet<InvoiceDetailEntity> InvoiceDetail { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Only use to create migration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.development.json", optional: true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProductDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImagesEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceDetailsEntityConfiguration());
        }
    }
}
