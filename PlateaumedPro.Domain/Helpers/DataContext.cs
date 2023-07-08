using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlateaumedPro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PlateaumedPro.Domain.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            // Noop
        }

        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CustomIdentity> CustomIdentities { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Here we create the indexes for each entity manually
            modelBuilder.Entity<AuditTrail>().HasIndex(u => new { u.Id });
            modelBuilder.Entity<User>().HasIndex(u => new { u.Id });
            modelBuilder.Entity<Teacher>().HasIndex(u => new { u.Id });
            modelBuilder.Entity<Student>().HasIndex(u => new { u.Id });

            // Using a Value Converter so we can store the enums as strings in the database (https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions)
          
            modelBuilder
               .Entity<AuditTrail>()
               .Property(m => m.ActionType)
               .HasConversion(new EnumToStringConverter<ActionType>());

          
            base.OnModelCreating(modelBuilder);
        }

    }
}
