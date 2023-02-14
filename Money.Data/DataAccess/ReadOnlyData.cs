
using System.Data;
using System.Text.RegularExpressions;

using Microsoft.EntityFrameworkCore;

using Money.Data.Dto;
using Money.Data.Entities;

namespace Money.Data.DataAccess;

public sealed class ReadOnlyData : DataAccessBase, IReadonlyData
{
    public ReadOnlyData(IDatabaseFileLocator databaseLocator) : base(databaseLocator)
    {
    }

    public Task<List<string>> GetCategoriesAsync()
    {
        using MoneyContext db = ConnectDatabase();
        return db
            .Categories
            .Select(c => c.Description)
            .Order()
            .ToListAsync();
    }

    public async Task<Statistics> GetStatisticsAsync(DateOnly start, DateOnly end)
    {
        using MoneyContext db = ConnectDatabase();
        List<Spending> data = await db
            .Spendings
            .Include(s => s.Category)
            .Where(x => x.Date >= start)
            .Where(x => x.Date <= end)
            .ToListAsync();

        Dictionary<DateOnly, double> dates = data
            .GroupBy(x => x.Date)
            .ToDictionary(x => x.Key, x => x.Sum(x => x.Ammount));

        Dictionary<string, double> categoreis = data
            .GroupBy(x => x.Category.Description)
            .ToDictionary(x => x.Key, x => x.Sum(x => x.Ammount));

        return new Statistics
        {
            SumPerDay = dates,
            Count = data.Count,
            SumPerCategory = categoreis,
        };
    }

    public Task<int> GetSpendingsCount()
    {
        using MoneyContext db = ConnectDatabase();
        return GetSpendingsCount(db);
    }

    public Task<List<DataRowUi>> Find(string what,
                                    string? category,
                                    DateOnly? startDate,
                                    DateOnly? endDate,
                                    bool isRegex)
    {
        using MoneyContext db = ConnectDatabase();

        IQueryable<Spending> query = db
            .Spendings
            .Include(s => s.Category)
            .AsQueryable();

        if (startDate != null)
            query = query.Where(x => x.Date >= startDate);

        if (endDate != null)
            query = query.Where(x => x.Date <= endDate);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(x => EF.Functions.Like(x.Category.Description, $"%{category}%"));

        if (!isRegex)
        {
            query = query.Where(x => EF.Functions.Like(x.Description, $"%{what}%"));
        }
        else
        {
            query = query.Where(x => Regex.IsMatch(x.Description, what));
        }

        return query
            .Select(spending => DtoAdapter.ToDataRowUi(spending))
            .ToListAsync();
    }

    public Task<List<DataRowExcel>> ExportAsync(DateOnly? start = null, DateOnly? end = null)
    {
        using MoneyContext db = ConnectDatabase();
        IQueryable<Spending> query = db
            .Spendings
            .Include(s => s.Category)
            .AsQueryable();

        if (start != null)
            query = query.Where(x => x.Date >= start);

        if (end != null)
            query = query.Where(x => x.Date <= end);

        return query
            .Select(spending => DtoAdapter.ToDataRowExcel(spending))
            .ToListAsync();
    }

    public async IAsyncEnumerable<DataRowBackup> ExportBackupAsync()
    {
        using MoneyContext db = ConnectDatabase();
        IQueryable<Spending> query = db
            .Spendings
            .Include(s => s.Category);

        await foreach (var item in query.AsAsyncEnumerable())
        {
            yield return DtoAdapter.ToDataRowBackup(item);
        }
    }

    public async IAsyncEnumerable<DataRowBackup> ExportBackupAsync(DateTime startDate)
    {
        using MoneyContext db = ConnectDatabase();
        IQueryable<Spending> query = db
            .Spendings
            .Include(s => s.Category)
            .Where(s => s.AddedOn >= startDate)
            .OrderBy(s => s.AddedOn);

        await foreach (var item in query.AsAsyncEnumerable())
        {
            yield return DtoAdapter.ToDataRowBackup(item);
        }
    }

    public Task<DateTime> GetLastInsertDate()
    {
        using MoneyContext db = ConnectDatabase();

        return db.Spendings
            .OrderByDescending(x => x.AddedOn)
            .Select(x => x.AddedOn)
            .FirstOrDefaultAsync();
    }
}
