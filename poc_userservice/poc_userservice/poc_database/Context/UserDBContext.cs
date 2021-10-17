using Microsoft.EntityFrameworkCore;
using poc_database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using poc_common.Configurations;

namespace poc_database.Context
{
    public class UserDBContext: DbContext
    {
        public readonly IConfiguration Configuration;
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasOne(e => e.Role)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.RoleId)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .HasMaxLength(250)
                    .IsRequired();
            });
        }
    }
}
