using ENSEKWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ENSEKWebApp.Data
{
        public class MeterReadingContext : DbContext
        {
            public MeterReadingContext(DbContextOptions<MeterReadingContext> options) : base(options)
            {

            }

            public DbSet<UserAccount> UserAccount { set; get; }
            public DbSet<MeterReadings> MeterReadings { set; get; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<UserAccount>(entity => { entity.HasIndex(e => e.AccountId).IsUnique(); });
                modelBuilder.Entity<MeterReadings>(entity => { entity.HasIndex(e => e.AccountId).IsUnique(); });
            }
        }
}
