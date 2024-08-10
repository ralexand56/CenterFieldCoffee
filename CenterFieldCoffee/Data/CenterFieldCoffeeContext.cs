using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CenterFieldCoffee.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CenterFieldCoffee.Data
{
    public class CenterFieldCoffeeContext : DbContext
    {
        public class NullableDateTimeAsUtcValueConverter() : ValueConverter<DateTime?, DateTime?>(
            v => !v.HasValue ? v : ToUtc(v.Value), v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v)
        {
            private static DateTime? ToUtc(DateTime v) => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime();
        }

        public class DateTimeAsUtcValueConverter() : ValueConverter<DateTime, DateTime>(
            v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));


        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            ArgumentNullException.ThrowIfNull(configurationBuilder);

            configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeAsUtcValueConverter>();
            configurationBuilder.Properties<DateTime?>().HaveConversion<NullableDateTimeAsUtcValueConverter>();
        }

        public CenterFieldCoffeeContext(DbContextOptions<CenterFieldCoffeeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var now = DateTime.Today;
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Name = "Store 1",
                    opening_hour = 6,
                    opening_minute = 30,
                    closing_hour = 18,
                    closing_minute = 0,
                },
                new Store
                {
                    Name = "Store 2",
                    opening_hour = 6,
                    opening_minute = 30,
                    closing_hour = 21,
                    closing_minute = 0,
                },
                new Store
                {
                    Name = "Store 4",
                    opening_hour = 16,
                    opening_minute = 30,
                    closing_hour = 20,
                    closing_minute = 0,
                }
            );
        }
        public DbSet<CenterFieldCoffee.Models.Store> Store { get; set; } = default!;
    }
}
