using Microsoft.EntityFrameworkCore;

using Money.Data.Entities;

namespace Money.Data
{
    internal sealed class MoneyContext : DbContext
    {
        public DbSet<Spending> Spendings { get; set; }

        public string DatabaseFile { get; }

        public MoneyContext()
        {
            DatabaseFile = Path.Combine(AppContext.BaseDirectory, "spendings.db");
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabaseFile}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoneyContext).Assembly);
        }
    }
}
