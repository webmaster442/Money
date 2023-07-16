using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using Money.Data.Entities;

namespace Money.Data;

internal sealed class MoneyContext : DbContext
{
    public DbSet<Spending> Spendings { get; set; }

    public DbSet<Category> Categories { get; set; }

    private readonly IDatabaseFileLocator _dbLocator;

    public MoneyContext() : this(new DatabaseFileLocator())
    {
    }

    public MoneyContext(IDatabaseFileLocator databaseFileLocator)
    {
        _dbLocator = databaseFileLocator;

        bool toMigrate = Database.GetPendingMigrations().Any();

        if (toMigrate)
            Database.Migrate();
    }

    public override void Dispose()
    {
        Database.CloseConnection();
        base.Dispose();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_dbLocator.DatabasePath}");
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(log => Debug.WriteLine(log));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoneyContext).Assembly);
    }
}
