using Microsoft.EntityFrameworkCore;

using Money.Data.Entities;

namespace Money.Data;

internal sealed class MoneyContext : DbContext
{
    public DbSet<Spending> Spendings { get; set; }

    public DbSet<Category> Categories { get; set; }

    private readonly IDatabaseFileLocator _dbLocator;


    public MoneyContext(IDatabaseFileLocator databaseFileLocator)
    {
        _dbLocator = databaseFileLocator;
        Database.EnsureCreated();
    }

    public override void Dispose()
    {
        Database.CloseConnection();
        base.Dispose();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbLocator.DatabasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoneyContext).Assembly);
    }
}
